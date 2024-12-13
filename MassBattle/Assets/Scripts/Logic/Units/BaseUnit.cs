using System.Collections;
using MassBattle.Core.Engine;
using MassBattle.Logic.Armies;
using MassBattle.Logic.Battle.Setup;
using MassBattle.Logic.Databases.Colors;
using MassBattle.Logic.Providers;
using MassBattle.Logic.Strategies;
using MassBattle.Logic.Utilities;
using UnityEngine;
using Zenject;

namespace MassBattle.Logic.Units
{
    public abstract class BaseUnit : BaseSceneEntity
    {
        private static readonly int COLOR = Shader.PropertyToID("_BaseColor");
        private static readonly int ATTACK = Animator.StringToHash("Attack");
        private static readonly int MOVEMENT_SPEED = Animator.StringToHash("MovementSpeed");
        private static readonly int TAKE_DAMAGE = Animator.StringToHash("TakeDamage");
        private static readonly int DEATH = Animator.StringToHash("Death");

        [SerializeField]
        protected float _health = 50f;
        [SerializeField]
        protected float _defense;
        [SerializeField]
        protected float _movementSpeed = 2f;
        [SerializeField]
        private float _avoidUnitsRadius = 2f;

        [Space, SerializeField]
        protected float _attackValue = 20f;
        [SerializeField]
        protected float _attackRange = 2.5f;
        [SerializeField]
        protected float _attackCooldown = 1f;
        [SerializeField]
        protected float _postAttackDelay;

        [Space, SerializeField]
        private bool _useBleeding;
        [SerializeField]
        private int _bleedingHealthReduction;
        [SerializeField]
        private float _bleedingCooldown;

        [Space, SerializeField]
        protected Animator _animator;
        [SerializeField]
        private Renderer _renderer;

        public float AttackValue => _attackValue;
        public float AttackRange => _attackRange;
        public ArmyData ArmyData => _cachedArmyData ??= _armyProvider.FindArmyBy(_armyId);
        private bool IsUnitAlive => _health > 0;
        public string UnitId { get; private set; }

        private IArmyProvider _armyProvider;
        protected IUnitsFactory _unitsFactory;
        private IColorDatabase _colorDatabase;

        private string _armyId;
        private ArmyData _cachedArmyData;
        private Color _armyColor;
        private IStrategy _strategy;
        private MaterialPropertyBlock _materialPropertyBlock;

        private BaseUnit _cachedNearestEnemy;
        private float _timeSinceLastAttack;
        private Vector3 _lastPosition;
        private Coroutine _bleedingProcess;

        [Inject]
        private void Construct(IArmyProvider armyProvider, IUnitsFactory unitsFactory, IColorDatabase colorDatabase)
        {
            _armyProvider = armyProvider;
            _unitsFactory = unitsFactory;
            _colorDatabase = colorDatabase;
        }

        public void Initialize(string unitId, InitialArmyData initialArmyData)
        {
            UnitId = unitId;

            _armyId = initialArmyData.DescriptorId;
            _armyColor = initialArmyData.ArmyColor;
            _strategy = CreateStrategy(initialArmyData.StrategyType);

            CalculateInitialTimeSinceLastAttack();
            UpdateColor(_armyColor);
            TryToActivateBleeding();
        }

        protected abstract IStrategy CreateStrategy(StrategyType strategyType);

        private void CalculateInitialTimeSinceLastAttack()
        {
            _timeSinceLastAttack = _postAttackDelay;
        }

        private void UpdateColor(Color color)
        {
            _materialPropertyBlock ??= new MaterialPropertyBlock();
            _materialPropertyBlock.SetColor(COLOR, color);
            _renderer.SetPropertyBlock(_materialPropertyBlock);
        }

        private void TryToActivateBleeding()
        {
            if (_useBleeding)
            {
                _bleedingProcess = StartCoroutine(BleedingProcess());
            }
        }

        private IEnumerator BleedingProcess()
        {
            WaitForSeconds delay = new WaitForSeconds(_bleedingCooldown);

            while (true)
            {
                yield return delay;
                ApplyDamage(_bleedingHealthReduction);
            }
        }

        private void ApplyDamage(float damage)
        {
            int animationTriggerToSet = TAKE_DAMAGE;
            _health -= Mathf.Max(damage, 0);

            if (IsUnitAlive == false)
            {
                animationTriggerToSet = DEATH;
                ArmyData.RemoveUnit(this);
            }

            _animator.SetTrigger(animationTriggerToSet);
        }

        public void ManualUpdate()
        {
            if (IsUnitAlive)
            {
                UpdateCooldown();
                TryMove(_cachedNearestEnemy);
                TryAttack(_cachedNearestEnemy);
            }
        }

        public void CacheNearestEnemy()
        {
            _cachedNearestEnemy = PositionFinder.FindNearestUnit(this, ArmyData.EnemyArmiesData);
        }

        private void UpdateCooldown()
        {
            _timeSinceLastAttack += Time.deltaTime;
        }

        private void TryMove(BaseUnit enemy)
        {
            if (CanMove(enemy))
            {
                Vector3 moveDirection = FindMoveDirection(enemy);
                Vector3 evadeDirection = FindEvadeOtherUnitsDirection(enemy);

                Vector3 averageMoveDirection = (moveDirection + evadeDirection) * 0.5f;
                float speed = _movementSpeed * Time.deltaTime;
                _transform.position += averageMoveDirection * speed;
                _transform.forward = moveDirection;
            }

            UpdateAnimatorMovementSpeed();
        }

        private bool CanMove(BaseUnit enemy) => enemy != null && _timeSinceLastAttack >= _postAttackDelay;
        private Vector3 FindMoveDirection(BaseUnit enemy) => _strategy.FindMoveDirection(enemy);

        private Vector3 FindEvadeOtherUnitsDirection(BaseUnit enemy)
        {
            Vector3 unitPosition = _transform.position;
            Vector3 alliesCenter = FindCenterOfAlliesInRange();

            Vector3 offsetToAllies = unitPosition - alliesCenter;
            Vector3 offsetToEnemy = unitPosition - enemy._transform.position;

            Vector3 evadeOffset;

            if (offsetToEnemy.magnitude <= _avoidUnitsRadius)
            {
                evadeOffset = (offsetToAllies + offsetToEnemy) * 0.5f;
            }
            else
            {
                evadeOffset = offsetToAllies;
            }

            return evadeOffset.normalized;
        }

        private Vector3 FindCenterOfAlliesInRange()
        {
            return PositionFinder.FindCenterOfUnitsInRange(this, ArmyData.AllUnits, _avoidUnitsRadius);
        }

        private void UpdateAnimatorMovementSpeed()
        {
            Vector3 position = _transform.position;

            float movementDistance = (position - _lastPosition).magnitude;
            float speed = movementDistance / (_movementSpeed * Time.deltaTime);
            _animator.SetFloat(MOVEMENT_SPEED, speed);

            _lastPosition = position;
        }

        private void TryAttack(BaseUnit enemy)
        {
            if (CanAttack(enemy))
            {
                _timeSinceLastAttack = 0f;
                _animator.SetTrigger(ATTACK);

                PerformAttack(enemy);
            }
        }

        private bool CanAttack(BaseUnit enemy)
        {
            bool isEnemyInAttackRange = false;

            if (enemy != null)
            {
                isEnemyInAttackRange = Vector3.Distance(_transform.position, enemy._transform.position) < _attackRange;
            }

            return IsEnoughTimeSinceLastAttack() && isEnemyInAttackRange;
        }

        public bool IsEnoughTimeSinceLastAttack() => _timeSinceLastAttack >= _attackCooldown;

        protected abstract void PerformAttack(BaseUnit enemy);

        public void TakeDamage(IAttack attacker)
        {
            float damage = CalculateDamage(attacker);
            ApplyDamage(damage);

            if (IsUnitAlive == false)
            {
                TurnUnitTo(attacker);
            }
        }

        private float CalculateDamage(IAttack attacker)
        {
            return attacker.AttackValue - _defense;
        }

        private void TurnUnitTo(IAttack attacker)
        {
            _transform.forward = attacker.AttackerPosition - _transform.position;
        }

        public void TakeDamageAnimationStart() => UpdateColor(_colorDatabase.DamageColor);
        public void TakeDamageAnimationFinish() => UpdateColor(_armyColor);
        public void DeathAnimationStart() => UpdateColor(_colorDatabase.DeathColor);
        public void DeathAnimationFinish() => Destroy(_gameObject);

        public override bool IsSetupCorrect()
        {
            bool isSetupCorrect = _health > 0f;
            isSetupCorrect &= _movementSpeed > 0f;
            isSetupCorrect &= _attackCooldown > 0f;
            isSetupCorrect &= _animator != null;
            isSetupCorrect &= _renderer != null;

            if (_useBleeding)
            {
                isSetupCorrect &= _bleedingHealthReduction > 0f;
                isSetupCorrect &= _bleedingCooldown > 0f;
            }

            return isSetupCorrect;
        }

        private void OnDestroy()
        {
            DisposeCoroutines();
        }

        private void DisposeCoroutines()
        {
            if (_bleedingProcess != null)
            {
                StopCoroutine(_bleedingProcess);
                _bleedingProcess = null;
            }
        }
    }
}

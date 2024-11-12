using MassBattle.Core.Entities.Engine;
using MassBattle.Logic.Armies;
using MassBattle.Logic.Databases;
using MassBattle.Logic.Providers;
using MassBattle.Logic.Strategies;
using MassBattle.Logic.Utilities;
using UnityEngine;

namespace MassBattle.Logic.Units
{
    public abstract class BaseUnit : ExtendedMonoBehaviour
    {
        private static readonly int COLOR = Shader.PropertyToID("_Color");
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
        protected Animator _animator;
        [SerializeField]
        private Renderer _renderer;

        public float AttackValue => _attackValue;
        public float AttackRange => _attackRange;
        public ArmyData ArmyData => _cachedArmyData ??= _armyProvider.FindArmyBy(_armyId);
        private bool IsUnitAlive => _health > 0;

        private IArmyProvider _armyProvider;
        protected IUpdateProvider _updateProvider;
        protected IUnitsFactory _unitsFactory;
        private IColorDatabase _colorDatabase;

        private string _armyId;
        private ArmyData _cachedArmyData;
        private Color _armyColor;
        private IStrategy _strategy;
        private MaterialPropertyBlock _materialPropertyBlock;

        private float _timeSinceLastAttack;
        private Vector3 _lastPosition;

        public void Initialize(
                ArmySetup armySetup, IArmyProvider armyProvider, IUpdateProvider updateProvider,
                IUnitsFactory unitsFactory, IColorDatabase colorDatabase)
        {
            _armyProvider = armyProvider;
            _updateProvider = updateProvider;
            _unitsFactory = unitsFactory;
            _colorDatabase = colorDatabase;

            _armyId = armySetup.ArmyId;
            _armyColor = armySetup.ArmyColor;
            _strategy = CreateStrategy(armySetup.StrategyType);
            _materialPropertyBlock = new MaterialPropertyBlock();

            CalculateInitialTimeSinceLastAttack();
            UpdateColor(_armyColor);
            AttachToEvents();
        }

        protected abstract IStrategy CreateStrategy(StrategyType strategyType);

        private void CalculateInitialTimeSinceLastAttack()
        {
            _timeSinceLastAttack = _postAttackDelay;
        }

        private void UpdateColor(Color color)
        {
            _materialPropertyBlock.SetColor(COLOR, color);
            _renderer.SetPropertyBlock(_materialPropertyBlock);
        }

        private void AttachToEvents()
        {
            _updateProvider.OnEarlyUpdate += CachePosition;
            _updateProvider.OnUpdate += ManualUpdate;
        }

        private void ManualUpdate()
        {
            if (IsUnitAlive)
            {
                BaseUnit nearestEnemy = FindNearestEnemy();
                UpdateCooldown();
                TryMove(nearestEnemy);
                TryAttack(nearestEnemy);
            }
        }

        private BaseUnit FindNearestEnemy() => PositionFinder.FindNearestUnit(this, ArmyData.EnemyArmiesData);

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
            int animationTriggerToSet = TAKE_DAMAGE;
            _health = CalculateNewHealth(attacker);

            if (IsUnitAlive == false)
            {
                animationTriggerToSet = DEATH;

                TurnUnitTo(attacker);
                ArmyData.RemoveUnit(this);
            }

            _animator.SetTrigger(animationTriggerToSet);
        }

        private float CalculateNewHealth(IAttack attacker)
        {
            float damage = attacker.AttackValue - _defense;
            return _health - Mathf.Max(damage, 0);
        }

        private void TurnUnitTo(IAttack attacker)
        {
            _transform.forward = attacker.AttackerPosition - _transform.position;
        }

        public void TakeDamageAnimationStart() => UpdateColor(_colorDatabase.DamageColor);
        public void TakeDamageAnimationFinish() => UpdateColor(_armyColor);
        public void DeathAnimationStart() => UpdateColor(_colorDatabase.DeathColor);
        public void DeathAnimationFinish() => Destroy(_gameObject);

        private void OnDestroy()
        {
            DetachFromEvents();
        }

        private void DetachFromEvents()
        {
            if (_updateProvider != null)
            {
                _updateProvider.OnEarlyUpdate -= CachePosition;
                _updateProvider.OnUpdate -= ManualUpdate;
            }
        }
    }
}

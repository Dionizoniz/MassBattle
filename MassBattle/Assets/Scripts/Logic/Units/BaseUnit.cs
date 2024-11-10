using System.Linq;
using MassBattle.Logic.Armies;
using MassBattle.Logic.Setup;
using MassBattle.Logic.Strategies;
using MassBattle.Logic.Units.Weapons;
using MassBattle.Logic.Utilities;
using UnityEngine;

namespace MassBattle.Logic.Units
{
    public abstract class BaseUnit : MonoBehaviour
    {
        [SerializeField]
        protected float _health = 50f;
        [SerializeField]
        protected float _defense;
        [SerializeField]
        protected float _movementSpeed = 2f;

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

        private ArmyData _cachedArmyData;
        private IArmyProvider _armyProvider;
        private string _armyId;
        private IStrategy _strategy;

        private float _timeSinceLastAttack;
        private Vector3 _lastPosition;

        public void Initialize(IArmyProvider armyProvider, ArmySetup armySetup)
        {
            _armyProvider = armyProvider;
            _armyId = armySetup.ArmyId;
            _strategy = CreateStrategy(armySetup.StrategyType);

            CalculateInitialTimeSinceLastAttack();
            UpdateColor(armySetup.ArmyColor);
        }

        protected abstract IStrategy CreateStrategy(StrategyType strategyType);

        private void CalculateInitialTimeSinceLastAttack()
        {
            _timeSinceLastAttack = _postAttackDelay;
        }

        private void UpdateColor(Color color) // TODO optimize "_Color"
        {
            MaterialPropertyBlock propertyBlock = new();
            propertyBlock.SetColor("_Color", color);
            _renderer.SetPropertyBlock(propertyBlock);
        }

        private void Update()
        {
            if (IsUnitAlive())
            {
                BaseUnit nearestEnemy = FindNearestEnemy();
                UpdateCooldown();
                TryMove(nearestEnemy);
                TryAttack(nearestEnemy);
            }
        }

        private bool IsUnitAlive() => _health > 0;
        private BaseUnit FindNearestEnemy() => PositionFinder.FindNearestUnit(this, ArmyData.enemyArmyData);

        private void UpdateCooldown()
        {
            _timeSinceLastAttack += Time.deltaTime;
        }

        private void TryMove(BaseUnit enemy)
        {
            if (CanMove(enemy))
            {
                Vector3 moveDirection = FindMoveDirection(enemy);
                Vector3 evadeDirection = FindEvadeOtherUnitsDirection();

                Vector3 averageMoveDirection = (moveDirection + evadeDirection) * 0.5f;
                float speed = _movementSpeed * Time.deltaTime;
                transform.position += averageMoveDirection * speed;
            }

            UpdateAnimatorMovementSpeed();
        }

        private bool CanMove(BaseUnit enemy) => enemy != null && _timeSinceLastAttack >= _postAttackDelay;
        private Vector3 FindMoveDirection(BaseUnit enemy) => _strategy.FindMoveDirection(enemy);

        private Vector3 FindEvadeOtherUnitsDirection() // TODO refactor
        {
            var allUnits = ArmyData.FindAllUnits().Union(ArmyData.enemyArmyData.FindAllUnits()).ToList();
            Vector3 center = PositionFinder.FindCenterOf(allUnits);
            float centerDist = Vector3.Distance(gameObject.transform.position, center);
            Vector3 evadeOffset = Vector3.zero;

            if (centerDist > 80.0f)
            {
                Vector3 toNearest = (center - transform.position).normalized;
                evadeOffset -= toNearest * (80.0f - centerDist);
            }
            else
            {
                foreach (var obj in allUnits)
                {
                    float dist = Vector3.Distance(gameObject.transform.position, obj.transform.position);

                    if (dist < 2f)
                    {
                        Vector3 toNearest = (obj.transform.position - transform.position).normalized;
                        evadeOffset -= toNearest * (2.0f - dist);
                    }
                }
            }

            return evadeOffset.normalized;
        }

        private void UpdateAnimatorMovementSpeed() // TODO optimize "MovementSpeed"
        {
            Vector3 position = transform.position;
            float speed = _movementSpeed * Time.deltaTime;
            _animator.SetFloat("MovementSpeed", (position - _lastPosition).magnitude / speed);
            _lastPosition = position;
        }

        private void TryAttack(BaseUnit enemy) // TODO optimize "Attack"
        {
            if (CanAttack(enemy))
            {
                _timeSinceLastAttack = 0f;
                _animator.SetTrigger("Attack");

                PerformAttack(enemy);
            }
        }

        private bool CanAttack(BaseUnit enemy)
        {
            bool isEnemyInAttackRange = false;

            if (enemy != null)
            {
                isEnemyInAttackRange = Vector3.Distance(transform.position, enemy.transform.position) < _attackRange;
            }

            return IsEnoughTimeSinceLastAttack() && isEnemyInAttackRange;
        }

        public bool IsEnoughTimeSinceLastAttack() => _timeSinceLastAttack >= _attackCooldown;

        protected abstract void PerformAttack(BaseUnit enemy);

        public void TakeDamage(IAttack attacker) // TODO Optimize & Refactor & Convert to interface !!!
        {
            _health -= Mathf.Max(attacker.AttackValue - _defense, 0);

            if (IsUnitAlive())
            {
                _animator.SetTrigger("Hit");
            }
            else
            {
                transform.forward = attacker.AttackPosition - transform.position;
                ArmyData.RemoveUnit(this);

                _animator.SetTrigger("Death");
            }
        }

        public void OnDeathAnimFinished() // TODO rename
        {
            Destroy(gameObject);
        }
    }
}

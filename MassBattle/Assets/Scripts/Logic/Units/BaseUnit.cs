using System.Collections.Generic;
using System.Linq;
using MassBattle.Logic.Armies;
using MassBattle.Logic.Setup;
using MassBattle.Logic.Utilities;
using MassBattle.Logic.Weapons;
using UnityEngine;

namespace MassBattle.Logic.Units
{
    public abstract class BaseUnit : MonoBehaviour
    {
        [SerializeField]
        protected float _health = 50f;
        [SerializeField]
        protected float _speed = 0.1f;
        [SerializeField]
        protected float _defense;

        [Space, SerializeField]
        protected float _attack = 20f;
        [SerializeField]
        protected float _attackRange = 2.5f;
        [SerializeField]
        protected float _maxAttackCooldown = 1f;
        [SerializeField]
        protected float _postAttackDelay;

        [Space, SerializeField]
        protected Animator _animator;
        [SerializeField]
        private Renderer _renderer;

        public float AttackValue => _attack;
        public ArmyData ArmyData => _cachedArmyData ??= _armyProvider.FindArmyBy(_armyId);

        private IArmyProvider _armyProvider;
        private ArmyData _cachedArmyData;
        private string _armyId;

        protected float _timeSinceLastAttack;
        private Vector3 _lastUnitPosition;

        public void Initialize(IArmyProvider armyProvider, ArmySetup armySetup)
        {
            _armyProvider = armyProvider;
            _armyId = armySetup.ArmyId;

            CalculateInitialTimeSinceLastAttack();
            UpdateColor(armySetup.ArmyColor);
        }

        private void CalculateInitialTimeSinceLastAttack()
        {
            _timeSinceLastAttack = _postAttackDelay;
        }

        private void UpdateColor(Color color)
        {
            MaterialPropertyBlock propertyBlock = new();
            propertyBlock.SetColor("_Color", color);
            _renderer.SetPropertyBlock(propertyBlock);
        }

        private void Update()
        {
            if (_health > 0)
            {
                UpdateCooldown();

                BaseUnit nearestEnemy = FindNearestEnemy();
                Vector3 moveDirection = FindMoveDirection(nearestEnemy);
                Vector3 evadeDirection = FindEvadeOtherUnitsDirection();

                Vector3 finalMoveDirection = (moveDirection + evadeDirection) / 2;
                Move(finalMoveDirection);

                // try attack if in range
                TryAttack(nearestEnemy);

                var position = transform.position;
                _animator.SetFloat("MovementSpeed", (position - _lastUnitPosition).magnitude / _speed);
                _lastUnitPosition = position;
            }
        }

        private void UpdateCooldown()
        {
            _timeSinceLastAttack += Time.deltaTime;
        }

        private BaseUnit FindNearestEnemy()
        {
            return PositionFinder.FindNearestUnit(this, ArmyData.enemyArmyData);
        }

        private Vector3 FindMoveDirection(BaseUnit enemy)
        {
            Vector3 moveDirection = Vector3.zero;

            if (ArmyData.ArmySetup.StrategyType == StrategyType.Defensive)
            {
                moveDirection = UpdateDefensive(enemy);
            }
            else if (ArmyData.ArmySetup.StrategyType == StrategyType.Basic)
            {
                moveDirection = UpdateBasic(enemy);
            }

            return moveDirection;
        }

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

        private void Move(Vector3 delta)
        {
            if (_timeSinceLastAttack >= _postAttackDelay)
            {
                transform.position += delta * _speed;
            }
        }

        private void TryAttack(BaseUnit enemy)
        {
            if (enemy != null && CanAttack(enemy))
            {
                _timeSinceLastAttack = 0f;
                _animator.SetTrigger("Attack");

                PerformAttack(enemy);
            }
        }

        private bool CanAttack(BaseUnit enemy)
        {
            return _timeSinceLastAttack >= _maxAttackCooldown &&
                   Vector3.Distance(transform.position, enemy.transform.position) < _attackRange;
        }

        protected abstract void PerformAttack(BaseUnit enemy);
        protected abstract Vector3 UpdateDefensive(BaseUnit enemy); // TODO move to strategy
        protected abstract Vector3 UpdateBasic(BaseUnit enemy); // TODO move to strategy

        public void Hit(GameObject sourceGo) // TODO Refactor & Convert to interface !!!
        {
            BaseUnit source = sourceGo.GetComponent<BaseUnit>();
            float sourceAttack = 0;

            if (source != null)
            {
                sourceAttack = source._attack;
            }
            else
            {
                Arrow arrow = sourceGo.GetComponent<Arrow>();
                sourceAttack = arrow.attack;
            }

            _health -= Mathf.Max(sourceAttack - _defense, 0);

            if (_health < 0)
            {
                transform.forward = sourceGo.transform.position - transform.position;

                switch (this) // TODO convert to send to provider instead of manually removing elements
                {
                    case Warrior:
                        ArmyData.RemoveWarrior(this as Warrior);
                        break;
                    case Archer:
                        ArmyData.RemoveArcher(this as Archer);
                        break;
                }

                _animator.SetTrigger("Death");
            }
            else
            {
                _animator.SetTrigger("Hit");
            }
        }

        public void OnDeathAnimFinished() // TODO rename
        {
            Destroy(gameObject);
        }
    }
}

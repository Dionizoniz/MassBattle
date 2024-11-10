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

        protected abstract void UpdateDefensive(List<BaseUnit> enemies);
        protected abstract void UpdateBasic(List<BaseUnit> enemies);

        public void Initialize(IArmyProvider armyProvider, ArmySetup armySetup)
        {
            _armyProvider = armyProvider;
            _armyId = armySetup.ArmyId;

            UpdateColor(armySetup.ArmyColor);
        }

        public void OnDeathAnimFinished()
        {
            Destroy(gameObject);
        }

        private void UpdateColor(Color color)
        {
            MaterialPropertyBlock propertyBlock = new();
            propertyBlock.SetColor("_Color", color);
            _renderer.SetPropertyBlock(propertyBlock);
        }

        protected void Move(Vector3 delta)
        {
            if (_timeSinceLastAttack >= _postAttackDelay)
            {
                transform.position += delta * _speed;
            }
        }

        public void Hit(GameObject sourceGo) // TODO Convert to interface !!!
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

        private void Update()
        {
            if (_health > 0)
            {
                List<BaseUnit> enemies = ArmyData.enemyArmyData.FindAllUnits();

                // TODO NEW UPDATE APPROACH
                // update cooldown
                UpdateCooldown();

                // calculate new target - from strategy (nearest) - all in first frame after that X per frame
                PositionFinder.FindNearestUnit(this, enemies, out BaseUnit nearestEnemy);

                // calculate new position - from strategy (base/defence)
                // calculate evade offset
                EvadeOtherUnits();

                // apply position + direction * speed * deltaTime
                // try attack if in range
                TryAttack(nearestEnemy);

                switch (ArmyData.ArmySetup.StrategyType)
                {
                    case StrategyType.Defensive:
                        UpdateDefensive(enemies);
                        break;
                    case StrategyType.Basic:
                        UpdateBasic(enemies);
                        break;
                }

                var position = transform.position;
                _animator.SetFloat("MovementSpeed", (position - _lastUnitPosition).magnitude / _speed);
                _lastUnitPosition = position;
            }
        }

        private void UpdateCooldown()
        {
            _timeSinceLastAttack += Time.deltaTime;
        }

        private void EvadeOtherUnits() // TODO refactor
        {
            var allUnits = ArmyData.FindAllUnits().Union(ArmyData.enemyArmyData.FindAllUnits()).ToList();
            Vector3 center = PositionFinder.FindCenterOf(allUnits);
            float centerDist = Vector3.Distance(gameObject.transform.position, center);

            if (centerDist > 80.0f)
            {
                Vector3 toNearest = (center - transform.position).normalized;
                transform.position -= toNearest * (80.0f - centerDist);
            }
            else
            {
                foreach (var obj in allUnits)
                {
                    float dist = Vector3.Distance(gameObject.transform.position, obj.transform.position);

                    if (dist < 2f)
                    {
                        Vector3 toNearest = (obj.transform.position - transform.position).normalized;
                        transform.position -= toNearest * (2.0f - dist);
                    }
                }
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
    }
}

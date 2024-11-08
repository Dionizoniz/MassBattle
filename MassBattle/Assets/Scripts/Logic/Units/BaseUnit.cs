using System.Collections.Generic;
using System.Linq;
using MassBattle.Logic.Armies;
using MassBattle.Logic.Installers;
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
        public ArmyData ArmyData => _cachedArmyData ??= _armyProvider.FindArmyBy(armyId);

        protected float _attackCooldown;
        private ArmyData _cachedArmyData;
        private IArmyProvider _armyProvider;

        private Vector3 _lastUnitPosition;

        public string armyId; // TODO improve access

        public abstract void Attack(BaseUnit enemy);

        protected abstract void UpdateDefensive(List<BaseUnit> allies, List<BaseUnit> enemies);
        protected abstract void UpdateBasic(List<BaseUnit> allies, List<BaseUnit> enemies);

        public void Initialize(IArmyProvider armyProvider)
        {
            _armyProvider = armyProvider;
        }

        public void SetColor(Color color) // TODO Update to use ArmyData 
        {
            MaterialPropertyBlock propertyBlock = new();
            propertyBlock.SetColor("_Color", color);
            _renderer.SetPropertyBlock(propertyBlock);
        }

        protected void Move(Vector3 delta)
        {
            if (_attackCooldown < _maxAttackCooldown - _postAttackDelay)
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
                        ArmyData.warriors.Remove(this as Warrior);
                        break;
                    case Archer:
                        ArmyData.archers.Remove(this as Archer);
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
                List<BaseUnit> allies = ArmyData.FindAllUnits();
                List<BaseUnit> enemies = ArmyData.enemyArmyData.FindAllUnits();

                UpdateBasicRules(allies, enemies);

                switch (ArmyData.ArmySetup.StrategyType)
                {
                    case StrategyType.Defensive:
                        UpdateDefensive(allies, enemies);
                        break;
                    case StrategyType.Basic:
                        UpdateBasic(allies, enemies);
                        break;
                }

                var position = transform.position;
                _animator.SetFloat("MovementSpeed", (position - _lastUnitPosition).magnitude / _speed);
                _lastUnitPosition = position;
            }
        }

        private void UpdateBasicRules(List<BaseUnit> allies, List<BaseUnit> enemies)
        {
            _attackCooldown -= Time.deltaTime;
            EvadeAllies(allies);
        }

        private void EvadeAllies(List<BaseUnit> allies)
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
    }
}

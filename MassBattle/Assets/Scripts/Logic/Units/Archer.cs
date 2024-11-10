using System.Collections.Generic;
using MassBattle.Logic.Utilities;
using MassBattle.Logic.Weapons;
using UnityEngine;

namespace MassBattle.Logic.Units
{
    public class Archer : BaseUnit
    {
        [Space, SerializeField]
        private Arrow _arrowPrefab;

        public override void Attack(BaseUnit enemy)
        {
            if (CanAttack(enemy))
            {
                _timeSinceLastAttack = 0f;
                Arrow spawnedArrow = Instantiate(_arrowPrefab);
                spawnedArrow.Initialize(this, enemy, ArmyData.ArmySetup.ArmyColor);

                _animator.SetTrigger("Attack");
            }
        }

        private bool CanAttack(BaseUnit enemy)
        {
            return _timeSinceLastAttack >= _maxAttackCooldown &&
                   Vector3.Distance(transform.position, enemy.transform.position) < _attackRange;
        }

        public void OnDeathAnimFinished()
        {
            Destroy(gameObject);
        }

        protected override void UpdateDefensive(List<BaseUnit> allies, List<BaseUnit> enemies)
        {
            Vector3 enemyCenter = PositionFinder.FindCenterOf(enemies);
            float distToEnemyX = Mathf.Abs(enemyCenter.x - transform.position.x);

            if (distToEnemyX > _attackRange)
            {
                if (enemyCenter.x < transform.position.x)
                {
                    Move(Vector3.left);
                }

                if (enemyCenter.x > transform.position.x)
                {
                    Move(Vector3.right);
                }
            }

            float distToNearest = PositionFinder.FindNearestUnit(this, enemies, out BaseUnit nearestEnemy);

            if (nearestEnemy != null)
            {
                if (distToNearest < _attackRange)
                {
                    Vector3 toNearest = (nearestEnemy.transform.position - transform.position).normalized;
                    toNearest.Scale(new Vector3(1, 0, 1));

                    Vector3 flank = Quaternion.Euler(0, 90, 0) * toNearest;
                    Move(-(toNearest + flank).normalized);
                }
                else
                {
                    Vector3 toNearest = (nearestEnemy.transform.position - transform.position).normalized;
                    toNearest.Scale(new Vector3(1, 0, 1));
                    Move(toNearest.normalized);
                }

                Attack(nearestEnemy);
            }
        }

        protected override void UpdateBasic(List<BaseUnit> allies, List<BaseUnit> enemies)
        {
            PositionFinder.FindNearestUnit(this, enemies, out BaseUnit nearestEnemy);

            if (nearestEnemy != null)
            {
                Vector3 toNearest = (nearestEnemy.transform.position - transform.position).normalized;
                toNearest.Scale(new Vector3(1, 0, 1));
                Move(toNearest.normalized);

                Attack(nearestEnemy);
            }
        }
    }
}

using System.Collections.Generic;
using MassBattle.Logic.Utilities;
using UnityEngine;

namespace MassBattle.Logic.Units
{
    public class Warrior : BaseUnit
    {
        public override void Attack(BaseUnit enemy)
        {
            if (CanAttack(enemy))
            {
                _attackCooldown = _maxAttackCooldown;
                _animator.SetTrigger("Attack");

                enemy.Hit(gameObject);
            }
        }

        private bool CanAttack(BaseUnit enemy)
        {
            return _attackCooldown <= 0 &&
                   Vector3.Distance(transform.position, enemy.transform.position) < _attackRange;
        }

        public void OnDeathAnimFinished()
        {
            Destroy(gameObject);
        }

        protected override void UpdateDefensive(List<BaseUnit> allies, List<BaseUnit> enemies)
        {
            Vector3 enemyCenter = PositionFinder.FindCenterOf(enemies);

            if (Mathf.Abs(enemyCenter.x - transform.position.x) > 20)
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

            PositionFinder.FindNearestUnit(this, enemies, out BaseUnit nearestObject);

            if (nearestObject != null)

            {
                if (_attackCooldown <= 0)
                {
                    Move((nearestObject.transform.position - transform.position).normalized);
                }
                else
                {
                    Move((nearestObject.transform.position - transform.position).normalized * -1);
                }

                Attack(nearestObject);
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

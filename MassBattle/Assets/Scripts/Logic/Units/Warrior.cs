using UnityEngine;

namespace MassBattle.Logic.Units
{
    public class Warrior : BaseUnit
    {
        protected override void PerformAttack(BaseUnit enemy)
        {
            enemy.Hit(gameObject);
        }

        protected override Vector3 UpdateDefensive(BaseUnit enemy)
        {
            Vector3 moveDirection;

            if (_timeSinceLastAttack >= _attackCooldown)
            {
                moveDirection = (enemy.transform.position - transform.position).normalized;
            }
            else
            {
                moveDirection = (enemy.transform.position - transform.position).normalized * -1;
            }

            return moveDirection;
        }

        protected override Vector3 UpdateBasic(BaseUnit enemy)
        {
            Vector3 toNearest = (enemy.transform.position - transform.position).normalized;
            toNearest.Scale(new Vector3(1, 0, 1));
            Vector3 moveDirection = toNearest.normalized;

            return moveDirection;
        }
    }
}

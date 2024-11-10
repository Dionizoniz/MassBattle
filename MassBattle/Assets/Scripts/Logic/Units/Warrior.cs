using UnityEngine;

namespace MassBattle.Logic.Units
{
    public class Warrior : BaseUnit
    {
        protected override void PerformAttack(BaseUnit enemy)
        {
            enemy.Hit(gameObject);
        }

        protected override void UpdateDefensive(BaseUnit enemy)
        {
            if (enemy != null)
            {
                if (_timeSinceLastAttack >= _maxAttackCooldown)
                {
                    Move((enemy.transform.position - transform.position).normalized);
                }
                else
                {
                    Move((enemy.transform.position - transform.position).normalized * -1);
                }
            }
        }

        protected override void UpdateBasic(BaseUnit enemy)
        {
            if (enemy != null)
            {
                Vector3 toNearest = (enemy.transform.position - transform.position).normalized;
                toNearest.Scale(new Vector3(1, 0, 1));
                Move(toNearest.normalized);
            }
        }
    }
}

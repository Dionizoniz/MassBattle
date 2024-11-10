using UnityEngine;
using MassBattle.Logic.Units;

namespace MassBattle.Logic.Strategies
{
    public class DefenceArcherStrategy : BaseArcherStrategy
    {
        public override Vector3 FindMoveDirection(BaseUnit owner, BaseUnit enemy)
        {
            Vector3 ownerPosition = owner.transform.position;
            Vector3 enemyPosition = enemy.transform.position;
            Vector3 moveDirection;

            if (Vector3.Distance(ownerPosition, enemyPosition) < owner.AttackRange)
            {
                Vector3 toNearest = (enemyPosition - ownerPosition).normalized;
                toNearest.Scale(new Vector3(1, 0, 1));

                Vector3 flank = Quaternion.Euler(0, 90, 0) * toNearest;
                moveDirection = -(toNearest + flank).normalized;
            }
            else
            {
                Vector3 toNearest = (enemyPosition - ownerPosition).normalized;
                toNearest.Scale(new Vector3(1, 0, 1));
                moveDirection = toNearest.normalized;
            }

            return moveDirection;
        }
    }
}

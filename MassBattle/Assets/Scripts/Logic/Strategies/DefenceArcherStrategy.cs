using UnityEngine;
using MassBattle.Logic.Units;

namespace MassBattle.Logic.Strategies
{
    public class DefenceArcherStrategy : BaseStrategy
    {
        public DefenceArcherStrategy(BaseUnit owner) : base(owner)
        { }

        public override Vector3 FindMoveDirection(BaseUnit enemy)
        {
            Vector3 offsetToEnemy = enemy._transform.position - OwnerPosition;
            Vector3 resultDirection = offsetToEnemy;

            if (offsetToEnemy.magnitude < _owner.AttackRange)
            {
                Vector3 flankOffset = Quaternion.Euler(0, 90, 0) * offsetToEnemy;
                resultDirection = (offsetToEnemy + flankOffset) * -1f;
            }

            return resultDirection.normalized;
        }
    }
}

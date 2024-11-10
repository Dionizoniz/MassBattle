using UnityEngine;
using MassBattle.Logic.Units;

namespace MassBattle.Logic.Strategies
{
    public class DefenceWarriorStrategy : BaseStrategy
    {
        public override Vector3 FindMoveDirection(BaseUnit owner, BaseUnit enemy)
        {
            Vector3 ownerPosition = owner.transform.position;
            Vector3 enemyPosition = enemy.transform.position;
            Vector3 moveDirection;

            if (owner.IsEnoughTimeSinceLastAttack())
            {
                moveDirection = (enemyPosition - ownerPosition).normalized;
            }
            else
            {
                moveDirection = (enemyPosition - ownerPosition).normalized * -1;
            }

            return moveDirection;
        }
    }
}

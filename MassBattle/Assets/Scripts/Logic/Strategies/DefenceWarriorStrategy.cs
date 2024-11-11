using UnityEngine;
using MassBattle.Logic.Units;

namespace MassBattle.Logic.Strategies
{
    public class DefenceWarriorStrategy : BaseStrategy
    {
        public DefenceWarriorStrategy(BaseUnit owner) : base(owner)
        { }

        public override Vector3 FindMoveDirection(BaseUnit enemy)
        {
            Vector3 moveDirection = (enemy.transform.position - OwnerPosition).normalized;

            if (_owner.IsEnoughTimeSinceLastAttack() == false)
            {
                moveDirection *= -1f;
            }

            return moveDirection;
        }
    }
}

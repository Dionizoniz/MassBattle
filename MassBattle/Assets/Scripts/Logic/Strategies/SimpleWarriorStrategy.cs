using UnityEngine;
using MassBattle.Logic.Units;

namespace MassBattle.Logic.Strategies
{
    public class SimpleStrategy : BaseStrategy
    {
        public override Vector3 FindMoveDirection(BaseUnit owner, BaseUnit enemy)
        {
            Vector3 toNearest = (enemy.transform.position - owner.transform.position).normalized;
            toNearest.Scale(new Vector3(1, 0, 1));
            Vector3 moveDirection = toNearest.normalized;

            return moveDirection;
        }
    }
}

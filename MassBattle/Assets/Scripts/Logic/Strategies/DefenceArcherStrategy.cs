using UnityEngine;
using MassBattle.Logic.Units;

namespace MassBattle.Logic.Strategies
{
    public class DefenceArcherStrategy : BaseArcherStrategy
    {
        public override Vector3 FindMoveDirection(BaseUnit enemy)
        {
            return Vector3.zero;
        }
    }
}

using UnityEngine;
using MassBattle.Logic.Units;

namespace MassBattle.Logic.Strategies
{
    public class SimpleStrategy : BaseStrategy
    {
        public SimpleStrategy(BaseUnit owner) : base(owner)
        { }

        public override Vector3 FindMoveDirection(BaseUnit enemy)
        {
            return (enemy.transform.position - OwnerPosition).normalized;
        }
    }
}

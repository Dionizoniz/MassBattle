using UnityEngine;
using MassBattle.Logic.Units;

namespace MassBattle.Logic.Strategies
{
    public abstract class BaseStrategy : IStrategy
    {
        public abstract Vector3 FindMoveDirection(BaseUnit owner, BaseUnit enemy);
    }
}

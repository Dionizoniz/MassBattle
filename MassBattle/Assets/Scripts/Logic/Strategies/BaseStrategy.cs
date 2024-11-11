using UnityEngine;
using MassBattle.Logic.Units;

namespace MassBattle.Logic.Strategies
{
    public abstract class BaseStrategy : IStrategy
    {
        protected readonly BaseUnit _owner;

        protected Vector3 OwnerPosition => _owner.transform.position;

        protected BaseStrategy(BaseUnit owner)
        {
            _owner = owner;
        }

        public abstract Vector3 FindMoveDirection(BaseUnit enemy);
    }
}

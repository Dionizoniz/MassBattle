using UnityEngine;
using MassBattle.Logic.Units;

namespace MassBattle.Logic.Strategies
{
    public interface IStrategy
    {
        Vector3 FindMoveDirection(BaseUnit owner, BaseUnit enemy);
    }
}

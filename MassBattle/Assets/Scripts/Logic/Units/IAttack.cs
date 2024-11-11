using UnityEngine;

namespace MassBattle.Logic.Units
{
    public interface IAttack
    {
        float AttackValue { get; }
        Vector3 AttackPosition { get; }
    }
}

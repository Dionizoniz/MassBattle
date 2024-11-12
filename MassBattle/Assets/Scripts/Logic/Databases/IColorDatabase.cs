using MassBattle.Core.Entities.Database;
using UnityEngine;

namespace MassBattle.Logic.Databases
{
    public interface IColorDatabase : IBaseDatabase<ColorData>
    {
        Color DamageColor { get; }
        Color DeathColor { get; }

        int FindColorIndex(Color color);
    }
}

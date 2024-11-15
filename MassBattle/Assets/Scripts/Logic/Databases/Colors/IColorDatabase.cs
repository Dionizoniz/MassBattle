using MassBattle.Core.Entities.Database;
using UnityEngine;

namespace MassBattle.Logic.Databases.Colors
{
    public interface IColorDatabase : IDatabase<ColorData>
    {
        Color DamageColor { get; }
        Color DeathColor { get; }

        int FindColorIndex(Color color);
    }
}

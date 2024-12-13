using System.Collections.Generic;
using MassBattle.Core.Databases;
using UnityEngine;

namespace MassBattle.Logic.Databases.Colors
{
    public interface IColorDatabase : IDatabase<ColorDescriptor>
    {
        Color DamageColor { get; }
        Color DeathColor { get; }
        List<ColorDescriptor> AllColors { get; }

        int FindColorIndex(Color color);
    }
}

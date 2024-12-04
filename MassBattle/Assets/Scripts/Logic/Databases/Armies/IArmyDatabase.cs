using System.Collections.Generic;
using MassBattle.Core.Databases;
using MassBattle.Logic.Databases.Colors;
using UnityEngine;

namespace MassBattle.Logic.Databases.Armies
{
    public interface IArmyDatabase : IDatabase<ColorDescriptor>
    {
        List<InitialArmyData> ArmiesData { get; }

        Vector2 UnitStackSizeRange { get; }

        void ClearSavedArmiesData();
        void SaveArmyData(InitialArmyData armyData);
    }
}

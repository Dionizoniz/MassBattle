using System.Collections.Generic;
using MassBattle.Core.Databases;
using MassBattle.Logic.Databases.Colors;

namespace MassBattle.Logic.Databases.Armies
{
    public interface IArmyDatabase : IDatabase<ColorDescriptor>
    {
        List<InitialArmyData> ArmiesData { get; }
        int MinUnitStackSize { get; }
        int MaxUnitStackSize { get; }

        void ClearSavedArmiesData();
        void SaveArmyData(InitialArmyData armyData);
    }
}

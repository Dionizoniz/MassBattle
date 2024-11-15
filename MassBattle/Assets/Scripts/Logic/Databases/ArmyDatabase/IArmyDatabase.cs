using System.Collections.Generic;
using MassBattle.Core.Entities.Database;

namespace MassBattle.Logic.Databases.ArmyDatabase
{
    public interface IArmyDatabase : IDatabase<InitialArmyData>
    {
        List<InitialArmyData> ArmiesData { get; }
        int MinUnitStackSize { get; }
        int MaxUnitStackSize { get; }

        void ClearSavedArmiesData();
        void SaveArmyData(InitialArmyData armyData);
    }
}

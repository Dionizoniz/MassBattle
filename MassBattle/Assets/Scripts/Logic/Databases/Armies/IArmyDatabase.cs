using System.Collections.Generic;
using MassBattle.Core.Databases;

namespace MassBattle.Logic.Databases.Armies
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

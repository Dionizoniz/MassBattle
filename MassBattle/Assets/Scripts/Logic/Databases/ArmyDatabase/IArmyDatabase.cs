using System.Collections.Generic;
using MassBattle.Core.Entities.Database;

namespace MassBattle.Logic.Databases.ArmyDatabase
{
    public interface IArmyDatabase : IDatabase<InitialArmyData>
    {
        List<ArmyData> SavedArmiesData { get; }
    }
}

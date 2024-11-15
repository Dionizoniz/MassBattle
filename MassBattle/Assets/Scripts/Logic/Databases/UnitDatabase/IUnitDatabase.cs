using System.Collections.Generic;
using MassBattle.Core.Entities.Database;

namespace MassBattle.Logic.Databases.UnitDatabase
{
    public interface IUnitDatabase : IDatabase<UnitData>
    {
        Dictionary<string, int> GenerateDefaultUnitsCountSetup(int defaultUnitStackSize);
    }
}

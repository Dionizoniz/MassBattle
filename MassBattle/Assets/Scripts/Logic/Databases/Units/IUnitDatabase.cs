using System.Collections.Generic;
using MassBattle.Core.Databases;
using MassBattle.Logic.Databases.Armies;

namespace MassBattle.Logic.Databases.Units
{
    public interface IUnitDatabase : IDatabase<UnitDescriptor>
    {
        IEnumerable<UnitDescriptor> AllUnits { get; }
        Dictionary<string, int> GenerateDefaultUnitsCountSetup(InitialArmyData initialArmyData);
    }
}

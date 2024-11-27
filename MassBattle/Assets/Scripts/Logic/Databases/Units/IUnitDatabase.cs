using System.Collections.Generic;
using MassBattle.Core.Databases;
using MassBattle.Logic.Databases.Colors;

namespace MassBattle.Logic.Databases.Units
{
    public interface IUnitDatabase : IDatabase<ColorDescriptor>
    {
        Dictionary<string, int> GenerateDefaultUnitsCountSetup(int defaultUnitStackSize);
    }
}

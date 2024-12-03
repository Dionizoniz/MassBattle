using System.Collections.Generic;
using System.Linq;
using MassBattle.Core.Databases;
using MassBattle.Logic.Databases.Armies;
using UnityEngine;

namespace MassBattle.Logic.Databases.Units
{
    [CreateAssetMenu(menuName = "Databases/" + nameof(UnitDatabase), fileName = nameof(UnitDatabase), order = -10000)]
    public class UnitDatabase : Database<UnitDescriptor>, IUnitDatabase
    {
        public IEnumerable<UnitDescriptor> AllUnits => _descriptors.Select(descriptor => descriptor);

        public Dictionary<string, int> GenerateDefaultUnitsCountSetup(InitialArmyData initialArmyData)
        {
            Dictionary<string, int> defaultUnitsCountSetup = new();

            foreach (var unitData in _descriptors)
            {
                defaultUnitsCountSetup.Add(unitData.DescriptorId, initialArmyData.DefaultUnitStackSize);
            }

            return defaultUnitsCountSetup;
        }
    }
}

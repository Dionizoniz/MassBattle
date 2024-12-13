using System.Collections.Generic;
using System.Linq;
using MassBattle.Core.Databases;
using MassBattle.Core.Providers;
using MassBattle.Logic.Battle.Setup;
using UnityEngine;

namespace MassBattle.Logic.Databases.Units
{
    [CreateAssetMenu(menuName = ConstantValues.DATABASE_MENU_GROUP + nameof(UnitDatabase),
                     fileName = nameof(UnitDatabase), order = ConstantValues.DATABASE_MENU_ORDER)]
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

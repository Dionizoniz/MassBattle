using System.Collections.Generic;
using MassBattle.Core.Databases;
using MassBattle.Logic.Databases.Colors;
using UnityEngine;

namespace MassBattle.Logic.Databases.Units
{
    [CreateAssetMenu(menuName = "Databases/" + nameof(UnitDatabase), fileName = nameof(UnitDatabase), order = -10000)]
    public class UnitDatabase : Database<ColorDescriptor>, IUnitDatabase
    {
        public Dictionary<string, int> GenerateDefaultUnitsCountSetup(int defaultUnitStackSize)
        {
            Dictionary<string, int> defaultUnitsCountSetup = new();

            foreach (var unitData in _elements)
            {
                defaultUnitsCountSetup.Add(unitData.DescriptorId, defaultUnitStackSize);
            }

            return defaultUnitsCountSetup;
        }
    }
}

using System.Collections.Generic;
using MassBattle.Core.Databases;
using UnityEngine;

namespace MassBattle.Logic.Databases.Units
{
    [CreateAssetMenu(menuName = "Databases/" + nameof(UnitDatabase), fileName = nameof(UnitDatabase), order = 0)]
    public class UnitDatabase : Database<UnitData>, IUnitDatabase
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

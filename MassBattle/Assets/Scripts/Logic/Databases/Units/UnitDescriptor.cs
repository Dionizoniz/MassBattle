using System;
using MassBattle.Core.Descriptors;
using MassBattle.Core.Entities;
using MassBattle.Logic.Units;
using UnityEngine;

namespace MassBattle.Logic.Databases.Units
{
    [Serializable]
    public class UnitDescriptor : Descriptor, IUnitDescriptor
    {
        [SerializeField]
        private string _unitName;
        [SerializeField]
        private BaseUnit _unitPrefabToSpawn;

        public string Name => _unitName;
        public BaseUnit UnitPrefabToSpawn => _unitPrefabToSpawn;

        public override bool IsSetupCorrect()
        {
            return _unitPrefabToSpawn != null && _unitPrefabToSpawn.IsSetupCorrect() && string.IsNullOrEmpty(_unitName);
        }
    }
}

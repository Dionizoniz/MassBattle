using MassBattle.Core.Descriptors;
using MassBattle.Logic.Units;
using UnityEngine;

namespace MassBattle.Logic.Databases.Units
{
    [CreateAssetMenu(menuName = "Descriptors/" + nameof(UnitDescriptor), fileName = nameof(UnitDescriptor),
                     order = -10000)]
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

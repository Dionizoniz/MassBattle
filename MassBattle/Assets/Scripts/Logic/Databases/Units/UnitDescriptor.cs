using MassBattle.Core.Descriptors;
using MassBattle.Core.Providers;
using MassBattle.Logic.Units;
using UnityEngine;

namespace MassBattle.Logic.Databases.Units
{
    [CreateAssetMenu(menuName = ConstantValues.DESCRIPTOR_MENU_GROUP + nameof(UnitDescriptor),
                     fileName = nameof(UnitDescriptor), order = ConstantValues.DESCRIPTOR_MENU_ORDER)]
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
            bool isSetupCorrect = true;

            isSetupCorrect &= _unitPrefabToSpawn != null && _unitPrefabToSpawn.IsSetupCorrect();
            isSetupCorrect &= string.IsNullOrEmpty(_unitName) == false;

            return isSetupCorrect;
        }
    }
}

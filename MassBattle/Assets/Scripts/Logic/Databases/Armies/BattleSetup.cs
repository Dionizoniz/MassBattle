using System.Collections.Generic;
using MassBattle.Core.Engine;
using MassBattle.Core.Providers;
using MassBattle.Logic.Databases.Colors;
using UnityEngine;

namespace MassBattle.Logic.Databases.Armies
{
    [CreateAssetMenu(menuName = ConstantValues.SETUP_MENU_GROUP + nameof(BattleSetup),
                     fileName = nameof(BattleSetup), order = ConstantValues.SETUP_MENU_ORDER)]
    public class BattleSetup : ExtendedScriptableObject, IBattleSetup
    {
        // TODO implement
        // min units
        // max units
        // default units 
        // max army count
        // default active army count

        // saved armies data

        [SerializeField]
        protected List<InitialArmyData> _elements = new();
        [Space, SerializeField]
        private int _minUnitStackSize;
        [SerializeField]
        private int _maxUnitStackSize = 150;

        private readonly List<InitialArmyData> _savedArmiesData = new();
        private bool UseSavedArmiesData => _savedArmiesData != null && _savedArmiesData.Count > 0; // TODO remove
        public List<InitialArmyData> ArmiesData => UseSavedArmiesData ? _savedArmiesData : _elements;

        public Vector2 UnitStackSizeRange => new(_minUnitStackSize, _maxUnitStackSize);

        public void SaveArmyData(InitialArmyData armyData)
        {
            int index = FindSavedArmyDataIndexBy(armyData.DescriptorId);

            if (index >= 0)
            {
                _savedArmiesData.RemoveAt(index);
            }

            _savedArmiesData.Add(armyData);
        }

        private int FindSavedArmyDataIndexBy(string id)
        {
            return _savedArmiesData.FindIndex(armyData => armyData.DescriptorId == id);
        }

        public void ClearSavedArmiesData()
        {
            _savedArmiesData.Clear();
        }

        public bool IsSetupCorrect() // TODO
        {
            // bool isSetupCorrect = base.IsSetupCorrect();
            //
            // foreach (var initialArmyData in _descriptors)
            // {
            //     //    isSetupCorrect &= initialArmyData.IsSetupCorrect(); TODO restore code
            //
            //     //    int unitStackSize = initialArmyData.DefaultUnitStackSize;
            //     //    isSetupCorrect &= unitStackSize >= MinUnitStackSize && unitStackSize <= MaxUnitStackSize;
            // }

            return true;
        }
    }
}

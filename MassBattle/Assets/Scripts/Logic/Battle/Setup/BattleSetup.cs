using System.Collections.Generic;
using MassBattle.Core.Engine;
using MassBattle.Core.Providers;
using UnityEngine;

namespace MassBattle.Logic.Battle.Setup
{
    [CreateAssetMenu(menuName = ConstantValues.SETUP_MENU_GROUP + nameof(BattleSetup), fileName = nameof(BattleSetup),
                     order = ConstantValues.SETUP_MENU_ORDER)]
    public class BattleSetup : ExtendedScriptableObject, IBattleSetup
    {
        [Space, SerializeField]
        private int _minUnitStackSize;
        [SerializeField]
        private int _maxUnitStackSize = 150;
        [SerializeField]
        private int _defaultUnitStackSize = 100;

        [Space, SerializeField]
        private int _maxArmiesCount = 4;
        [SerializeField]
        private int _defaultActiveArmiesCount = 4;

        public Vector2 UnitStackSizeRange => new(_minUnitStackSize, _maxUnitStackSize);
        public List<InitialArmyData> SavedArmiesData { get; private set; }

        public void SaveArmyData(InitialArmyData armyData)
        {
            int index = FindSavedArmyDataIndexBy(armyData.DescriptorId);

            if (index >= 0)
            {
                SavedArmiesData.RemoveAt(index);
            }

            SavedArmiesData.Add(armyData);
        }

        private int FindSavedArmyDataIndexBy(string id)
        {
            return SavedArmiesData.FindIndex(data => data.DescriptorId == id);
        }

        public void ClearSavedArmiesData()
        {
            SavedArmiesData.Clear();
        }

        public bool IsSetupCorrect() // TODO improve
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

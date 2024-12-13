using System.Collections.Generic;
using MassBattle.Core.Engine;
using MassBattle.Core.Providers;
using MassBattle.Logic.Databases.Colors;
using MassBattle.Logic.Databases.Units;
using MassBattle.Logic.Strategies;
using UnityEngine;
using Zenject;

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
        private int _defaultActiveArmiesCount = 2;

        [Space, SerializeField]
        private ColorDatabase _colorDatabase;
        [SerializeField]
        private UnitDatabase _unitDatabase;

        public Vector2 UnitStackSizeRange => new(_minUnitStackSize, _maxUnitStackSize);
        private bool UseSavedArmiesData => _savedArmiesData != null && _savedArmiesData.Count > 0;
        public List<InitialArmyData> ArmiesData => UseSavedArmiesData ? _savedArmiesData : GenerateDefaultArmies();

        private readonly List<InitialArmyData> _savedArmiesData = new();

        private List<InitialArmyData> GenerateDefaultArmies()
        {
            List<InitialArmyData> defaultArmies = new();

            for (int i = 0; i < _maxArmiesCount; i++) // TODO improve
            {
                InitialArmyData data = new(i.ToString(), $"Army {i}",
                                           _unitDatabase.GenerateDefaultUnitsCountSetup(_defaultUnitStackSize),
                                           StrategyType.Basic, _colorDatabase.AllColors[i].Color,
                                           i < _defaultActiveArmiesCount);

                defaultArmies.Add(data);
            }

            return defaultArmies;
        }

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
            return _savedArmiesData.FindIndex(data => data.DescriptorId == id);
        }

        public void ClearSavedArmiesData()
        {
            _savedArmiesData.Clear();
        }

        public bool IsSetupCorrect() // TODO improve
        {
            bool isSetupCorrect =
                    _defaultUnitStackSize >= _minUnitStackSize && _defaultUnitStackSize <= _maxUnitStackSize;

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

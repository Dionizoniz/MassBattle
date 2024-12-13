using System.Collections.Generic;
using MassBattle.Core.Engine;
using MassBattle.Core.ExtensionMethods;
using MassBattle.Core.Providers;
using MassBattle.Logic.Databases.Colors;
using MassBattle.Logic.Databases.Units;
using MassBattle.Logic.Strategies;
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
        private int _defaultActiveArmiesCount = 2;
        [SerializeField]
        private string _defaultArmyNamePrefix = "Army";

        [Space, SerializeField]
        private StrategyType _defaultStrategyType = StrategyType.Basic;

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

            for (int i = 0; i < _maxArmiesCount; i++)
            {
                InitialArmyData data = GenerateDefaultArmy(i);
                defaultArmies.Add(data);
            }

            return defaultArmies;
        }

        private InitialArmyData GenerateDefaultArmy(int index)
        {
            Dictionary<string, int> unitsCountSetup = _unitDatabase.GenerateUnitsCountSetup(_defaultUnitStackSize);
            Color color = _colorDatabase.AllColors[index].Color;
            bool isActive = index < _defaultActiveArmiesCount;

            return new InitialArmyData(FindArmyId(index), FindArmyName(index), unitsCountSetup, _defaultStrategyType,
                                       color, isActive);
        }

        private string FindArmyId(int index) => index.ToString();
        private string FindArmyName(int index) => $"{_defaultArmyNamePrefix} {index + 1}";

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

        public bool IsSetupCorrect()
        {
            bool isCorrectStackSetup = _defaultUnitStackSize.IsInRange(_minUnitStackSize, _maxUnitStackSize);
            bool isCorrectArmySetup = _defaultActiveArmiesCount <= _maxArmiesCount;

            return isCorrectStackSetup && isCorrectArmySetup && _colorDatabase != null && _unitDatabase != null;
        }
    }
}

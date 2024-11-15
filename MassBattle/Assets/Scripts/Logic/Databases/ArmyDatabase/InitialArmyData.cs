using System;
using System.Collections.Generic;
using MassBattle.Core.Entities.Database;
using MassBattle.Core.Entities.Tests;
using MassBattle.Logic.Strategies;
using UnityEngine;

namespace MassBattle.Logic.Databases.ArmyDatabase
{
    [Serializable]
    public class InitialArmyData : BaseData, ICheckSetup
    {
        [SerializeField]
        private string _armyName;
        [SerializeField]
        private int _defaultUnitStackSize;
        [SerializeField]
        private StrategyType _strategyType;
        [SerializeField]
        private Color _armyColor = Color.white;
        [SerializeField]
        private bool _isArmyActive;

        // TODO info comment to save???
        private Dictionary<string, int> _unitsCountSetup = new();

        public string ArmyName => _armyName;
        public int DefaultUnitStackSize => _defaultUnitStackSize;
        public StrategyType StrategyType => _strategyType;
        public Color ArmyColor => _armyColor;
        public bool IsArmyActive => _isArmyActive;
        public Dictionary<string, int> UnitsCountSetup => _unitsCountSetup;

        protected override string ClassName => nameof(InitialArmyData);

        public InitialArmyData(
                string id, string armyName, Dictionary<string, int> unitsCountSetup, StrategyType strategyType,
                Color armyColor, bool isArmyActive)
        {
            OverrideId(id);
            _armyName = armyName;
            _strategyType = strategyType;
            _armyColor = armyColor;
            _isArmyActive = isArmyActive;
            _unitsCountSetup = unitsCountSetup;
        }

        public bool IsSetupCorrect()
        {
            bool isSetupCorrect = true;

            isSetupCorrect &= string.IsNullOrEmpty(_armyName) == false;
            isSetupCorrect &= _defaultUnitStackSize > 0;

            return isSetupCorrect;
        }
    }
}

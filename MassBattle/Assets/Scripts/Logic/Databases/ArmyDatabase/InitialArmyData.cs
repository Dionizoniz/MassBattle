using System;
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
        private Color _armyColor;
        [SerializeField]
        private bool _isArmyActive;

        public string ArmyName => _armyName;
        public int DefaultUnitStackSize => _defaultUnitStackSize;
        public StrategyType StrategyType => _strategyType;
        public Color ArmyColor => _armyColor;
        public bool IsArmyActive => _isArmyActive;

        protected override string ClassName => nameof(InitialArmyData);

        public bool IsSetupCorrect()
        {
            bool isSetupCorrect = true;

            isSetupCorrect &= string.IsNullOrEmpty(_armyName) == false;

            return isSetupCorrect;
        }
    }
}

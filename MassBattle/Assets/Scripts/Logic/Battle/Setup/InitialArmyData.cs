using System;
using System.Collections.Generic;
using MassBattle.Core.Databases;
using MassBattle.Core.Entities;
using MassBattle.Logic.Strategies;
using UnityEngine;

namespace MassBattle.Logic.Battle.Setup
{
    [Serializable]
    public class InitialArmyData : BaseData, IName
    {
        [SerializeField]
        private string _armyName;
        [SerializeField]
        private StrategyType _strategyType;
        [SerializeField]
        private Color _armyColor;
        [SerializeField]
        private bool _isArmyActive;

        public string Name => _armyName;
        public StrategyType StrategyType => _strategyType;
        public Color ArmyColor => _armyColor;
        public bool IsArmyActive => _isArmyActive;
        public Dictionary<string, int> UnitsCountSetup { get; }

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
            UnitsCountSetup = unitsCountSetup;
        }
    }
}

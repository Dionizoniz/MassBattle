using System;
using MassBattle.Core.Entities.Database;
using MassBattle.Core.Entities.Tests;
using MassBattle.Core.Utilities;
using MassBattle.Logic.Strategies;
using UnityEngine;

namespace MassBattle.Logic.Armies
{
    [Serializable]
    public class ArmySetup : ICheckSetup, IId
    {
        [SerializeField, ReadOnly]
        private string _id;
        [SerializeField]
        private string _armyName;

        [SerializeField]
        private int _warriorsCount;
        [SerializeField]
        private int _archersCount;
        [SerializeField]
        private StrategyType _strategyType;
        [SerializeField]
        private Color _armyColor;
        [SerializeField]
        private bool _isArmyActive;

        public string Id => _id;
        public string ArmyName => _armyName;
        public int WarriorsCount => _warriorsCount;
        public int ArchersCount => _archersCount;
        public StrategyType StrategyType => _strategyType;
        public Color ArmyColor => _armyColor;
        public bool IsArmyActive => _isArmyActive;

        public ArmySetup(
                int index, string armyName, int warriorsCount, int archersCount, StrategyType strategyType,
                Color armyColor, bool isArmyActive)
        {
            GenerateId(index);
            _armyName = armyName;
            _warriorsCount = warriorsCount;
            _archersCount = archersCount;
            _strategyType = strategyType;
            _armyColor = armyColor;
            _isArmyActive = isArmyActive;
        }

        public bool IsSetupCorrect()
        {
            bool isSetupCorrect = true;

            isSetupCorrect &= string.IsNullOrEmpty(_id) == false;
            isSetupCorrect &= string.IsNullOrEmpty(_armyName) == false;

            return isSetupCorrect;
        }

        public void GenerateId(int index)
        {
            _id = $"{nameof(ArmySetup)}{index}";
        }
    }
}

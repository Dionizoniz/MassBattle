using System;
using MassBattle.Logic.Strategies;
using UnityEngine;

namespace MassBattle.Logic.Armies
{
    [Serializable]
    public class ArmySetup
    {
        [SerializeField]
        private string _armyId;

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

        public string ArmyId => _armyId;
        public int WarriorsCount => _warriorsCount;
        public int ArchersCount => _archersCount;
        public StrategyType StrategyType => _strategyType;
        public Color ArmyColor => _armyColor;
        public bool IsArmyActive => _isArmyActive;

        public ArmySetup(
                string armyId, int warriorsCount, int archersCount, StrategyType strategyType, Color armyColor,
                bool isArmyActive)
        {
            _armyId = armyId;
            _warriorsCount = warriorsCount;
            _archersCount = archersCount;
            _strategyType = strategyType;
            _armyColor = armyColor;
            _isArmyActive = isArmyActive;
        }
    }
}

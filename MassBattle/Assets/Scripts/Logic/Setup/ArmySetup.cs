using System;
using UnityEngine;

namespace MassBattle.Logic.Setup
{
    [Serializable]
    public class ArmySetup
    {
        [SerializeField]
        private string armyId;

        [SerializeField]
        private int warriorsCount;
        [SerializeField]
        private int archersCount;
        [SerializeField]
        private StrategyType strategyType;
        [SerializeField]
        private Color armyColor;

        public string ArmyId => armyId;
        public int WarriorsCount => warriorsCount;
        public int ArchersCount => archersCount;
        public StrategyType StrategyType => strategyType;
        public Color ArmyColor => armyColor;

        public ArmySetup(string armyId, int warriorsCount, int archersCount, StrategyType strategyType, Color armyColor)
        {
            this.armyId = armyId;
            this.warriorsCount = warriorsCount;
            this.archersCount = archersCount;
            this.strategyType = strategyType;
            this.armyColor = armyColor;
        }
    }
}

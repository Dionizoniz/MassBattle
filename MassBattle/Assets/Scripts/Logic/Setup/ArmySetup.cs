using System;
using UnityEngine;

namespace MassBattle.Logic.Setup
{
    [Serializable]
    public class ArmySetup
    {
        [SerializeField]
        private string id;

        [SerializeField]
        private int warriorsCount;
        [SerializeField]
        private int archersCount;
        [SerializeField]
        private ArmyStrategy strategyType;

        public string Id => id;
        public int WarriorsCount => warriorsCount;
        public int ArchersCount => archersCount;
        public ArmyStrategy StrategyType => strategyType;

        public ArmySetup(string id, int warriorsCount, int archersCount, ArmyStrategy strategyType)
        {
            this.id = id;
            this.warriorsCount = warriorsCount;
            this.archersCount = archersCount;
            this.strategyType = strategyType;
        }
    }
}

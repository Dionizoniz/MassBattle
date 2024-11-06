using System.Collections.Generic;
using System.Linq;
using MassBattle.Logic.Units;
using UnityEngine;

namespace MassBattle.Logic.Armies
{
    public class ArmyData
    {
        public readonly string id;
        public readonly List<Warrior> warriors = new();
        public readonly List<Archer> archers = new();
        public readonly ArmyStrategy strategy;
        public readonly Color color;

        public ArmyData enemyArmyData;

        public ArmyData(string id, List<Warrior> warriors, List<Archer> archers, ArmyStrategy strategy, Color color)
        {
            this.id = id;
            this.warriors = warriors;
            this.archers = archers;
            this.strategy = strategy;
            this.color = color;
        }

        public int CalculateUnitsCount() => warriors.Count + archers.Count;

        public List<BaseUnit> FindAllUnits() // TODO Optimize
        {
            List<BaseUnit> units = new();
            units.AddRange(warriors);
            units.AddRange(archers);

            return units;
        }
    }
}

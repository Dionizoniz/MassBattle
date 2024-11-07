using System.Collections.Generic;
using MassBattle.Logic.Setup;
using MassBattle.Logic.Units;

namespace MassBattle.Logic.Armies
{
    public class ArmyData
    {
        public ArmySetup ArmySetup { get; private set; }

        public readonly List<Warrior> warriors; // TODO change access
        public readonly List<Archer> archers; // TODO change access

        public ArmyData enemyArmyData;

        public ArmyData(ArmySetup armySetup, List<Warrior> warriors, List<Archer> archers)
        {
            ArmySetup = armySetup;
            this.warriors = warriors;
            this.archers = archers;
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

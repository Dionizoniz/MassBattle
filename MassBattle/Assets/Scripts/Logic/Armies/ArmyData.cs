using System.Collections.Generic;
using MassBattle.Logic.Setup;
using MassBattle.Logic.Units;

namespace MassBattle.Logic.Armies
{
    public class ArmyData
    {
        public ArmySetup ArmySetup { get; private set; }

        private readonly List<Warrior> _warriors;
        private readonly List<Archer> _archers;

        public ArmyData enemyArmyData;

        public ArmyData(ArmySetup armySetup, List<Warrior> warriors, List<Archer> archers)
        {
            ArmySetup = armySetup;
            _warriors = warriors;
            _archers = archers;
        }

        public void RemoveWarrior(Warrior warrior)
        {
            _warriors.Remove(warrior);
        }

        public void RemoveArcher(Archer archer)
        {
            _archers.Remove(archer);
        }

        public int CalculateUnitsCount() => _warriors.Count + _archers.Count;

        public List<BaseUnit> FindAllUnits() // TODO Optimize
        {
            List<BaseUnit> units = new();
            units.AddRange(_warriors);
            units.AddRange(_archers);

            return units;
        }
    }
}

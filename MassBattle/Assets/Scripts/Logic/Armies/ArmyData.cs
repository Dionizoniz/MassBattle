using System.Collections.Generic;
using MassBattle.Logic.Units;

namespace MassBattle.Logic.Armies
{
    public class ArmyData
    {
        public ArmySetup ArmySetup { get; private set; }

        private readonly List<Warrior> _warriors;
        private readonly List<Archer> _archers;
        private List<BaseUnit> _allUnits;

        public ArmyData enemyArmyData;

        public ArmyData(ArmySetup armySetup, List<Warrior> warriors, List<Archer> archers)
        {
            ArmySetup = armySetup;
            _warriors = warriors;
            _archers = archers;

            CacheAllUnits();
        }

        private void CacheAllUnits()
        {
            _allUnits = new List<BaseUnit>();
            _allUnits.AddRange(_warriors);
            _allUnits.AddRange(_archers);
        }

        public List<BaseUnit> FindAllUnits() => _allUnits;

        public void RemoveUnit(BaseUnit unit)
        {
            if (unit is Warrior warrior)
            {
                RemoveWarrior(warrior);
            }
            else
            {
                RemoveArcher(unit as Archer);
            }
        }

        private void RemoveWarrior(Warrior warrior)
        {
            _warriors.Remove(warrior);
            _allUnits.Remove(warrior);
        }

        private void RemoveArcher(Archer archer)
        {
            _archers.Remove(archer);
            _allUnits.Remove(archer);
        }
    }
}

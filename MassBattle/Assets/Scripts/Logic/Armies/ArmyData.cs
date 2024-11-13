using System;
using System.Collections.Generic;
using MassBattle.Logic.Units;

namespace MassBattle.Logic.Armies
{
    public class ArmyData
    {
        public event Action OnUnitRemove = delegate { };

        public ArmySetup ArmySetup { get; private set; }
        public List<BaseUnit> AllUnits { get; private set; }
        public List<ArmyData> EnemyArmiesData { get; private set; }

        private readonly List<Warrior> _warriors;
        private readonly List<Archer> _archers;

        public ArmyData(ArmySetup armySetup, List<Warrior> warriors, List<Archer> archers)
        {
            ArmySetup = armySetup;
            _warriors = warriors;
            _archers = archers;

            CacheAllUnits();
        }

        private void CacheAllUnits()
        {
            AllUnits = new List<BaseUnit>();
            AllUnits.AddRange(_warriors);
            AllUnits.AddRange(_archers);
        }

        public void InjectEnemyArmies(List<ArmyData> enemyArmiesData)
        {
            EnemyArmiesData = enemyArmiesData;
        }

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
            AllUnits.Remove(warrior);

            OnUnitRemove.Invoke();
        }

        private void RemoveArcher(Archer archer)
        {
            _archers.Remove(archer);
            AllUnits.Remove(archer);

            OnUnitRemove.Invoke();
        }

        public void EarlyUpdateArmy()
        {
            for (int i = 0; i < AllUnits.Count; i++)
            {
                AllUnits[i].CachePosition();
            }
        }

        public void UpdateArmy()
        {
            for (int i = 0; i < AllUnits.Count; i++)
            {
                AllUnits[i].ManualUpdate();
            }
        }
    }
}

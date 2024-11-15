using System;
using System.Collections.Generic;
using MassBattle.Logic.Databases.ArmyDatabase;
using MassBattle.Logic.Units;
using UnityEngine;

namespace MassBattle.Logic.Armies
{
    public class ArmyData
    {
        private const int FRAMES_TO_CACHE_UNITS_NEAREST_ENEMY = 10;

        public event Action OnUnitRemove = delegate { };

        public InitialArmyData InitialArmyData { get; private set; }
        public List<BaseUnit> AllUnits { get; private set; }
        public List<ArmyData> EnemyArmiesData { get; private set; }

        private readonly List<Warrior> _warriors;
        private readonly List<Archer> _archers;

        private int _lastUpdatedIndex;

        public ArmyData(InitialArmyData initialArmyData, List<Warrior> warriors, List<Archer> archers)
        {
            InitialArmyData = initialArmyData;

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
            CacheNearestEnemyForArmy();
            CallManualUpdateForArmy();
        }

        private void CacheNearestEnemyForArmy()
        {
            float partSize = (float)AllUnits.Count / FRAMES_TO_CACHE_UNITS_NEAREST_ENEMY;
            int unitsCountToRefresh = Mathf.CeilToInt(partSize);

            for (int i = 0; i < unitsCountToRefresh; i++, _lastUpdatedIndex++)
            {
                _lastUpdatedIndex %= AllUnits.Count;
                AllUnits[_lastUpdatedIndex].CacheNearestEnemy();
            }
        }

        private void CallManualUpdateForArmy()
        {
            for (int i = 0; i < AllUnits.Count; i++)
            {
                AllUnits[i].ManualUpdate();
            }
        }
    }
}

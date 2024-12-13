using System;
using System.Collections.Generic;
using MassBattle.Core.Databases;
using MassBattle.Logic.Battle.Setup;
using MassBattle.Logic.Units;
using UnityEngine;

namespace MassBattle.Logic.Armies
{
    public class ArmyData : BaseData
    {
        private const int FRAMES_TO_CACHE_UNITS_NEAREST_ENEMY = 10;

        public event Action OnUnitRemove = delegate { };

        public InitialArmyData InitialArmyData { get; private set; }
        public List<BaseUnit> AllUnits { get; private set; }
        public List<ArmyData> EnemyArmiesData { get; private set; }

        protected override string ClassName => nameof(ArmyData);

        private readonly Dictionary<string, List<BaseUnit>> _spawnedUnits;
        private int _lastUpdatedIndex;

        public ArmyData(InitialArmyData initialArmyData, Dictionary<string, List<BaseUnit>> spawnedUnits)
        {
            OverrideId(initialArmyData.DescriptorId);

            InitialArmyData = initialArmyData;
            _spawnedUnits = spawnedUnits;

            CacheAllUnits();
        }

        private void CacheAllUnits()
        {
            AllUnits = new List<BaseUnit>();

            foreach (KeyValuePair<string, List<BaseUnit>> units in _spawnedUnits)
            {
                AllUnits.AddRange(units.Value);
            }
        }

        public void InjectEnemyArmies(List<ArmyData> enemyArmiesData)
        {
            EnemyArmiesData = enemyArmiesData;
        }

        public void RemoveUnit(BaseUnit unit)
        {
            if (_spawnedUnits.ContainsKey(unit.UnitId))
            {
                _spawnedUnits[unit.UnitId].Remove(unit);
            }

            AllUnits.Remove(unit);
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

using System;
using System.Collections.Generic;
using System.Linq;
using MassBattle.Logic.Units;
using MassBattle.Logic.Utilities;
using UnityEngine;

namespace MassBattle.Logic.Armies
{
    public class ArmyProvider : IArmyProvider
    {
        public event Action<ArmyData> OnLastArmyStay = delegate { };
        public event Action OnNoArmyStay = delegate { };

        private readonly List<ArmyData> _armiesData = new();

        ~ArmyProvider()
        {
            ClearArmies();
        }

        public void ClearArmies()
        {
            foreach (ArmyData armyData in _armiesData)
            {
                armyData.OnUnitRemove -= TryNotifyBattleResult;
            }

            _armiesData.Clear();
        }

        public void RegisterArmy(ArmyData armyData)
        {
            if (_armiesData.Contains(armyData))
            {
                armyData.OnUnitRemove -= TryNotifyBattleResult;
                _armiesData.Remove(armyData);
            }

            armyData.OnUnitRemove += TryNotifyBattleResult;
            _armiesData.Add(armyData);
        }

        private void TryNotifyBattleResult()
        {
            List<ArmyData> armiesWithUnits = _armiesData.FindAll(army => army.AllUnits.Count > 0);

            if (armiesWithUnits.Count == 1)
            {
                OnLastArmyStay.Invoke(armiesWithUnits.First());
            }
            else if (armiesWithUnits.Count == 0)
            {
                OnNoArmyStay.Invoke();
            }
        }

        public ArmyData FindArmyBy(string armyId)
        {
            return _armiesData.FirstOrDefault(army => army.ArmySetup.ArmyId == armyId);
        }

        public void InitializedRegisteredArmies()
        {
            FillUpEnemiesForRegisteredArmies();
            TryNotifyBattleResult();
        }

        private void FillUpEnemiesForRegisteredArmies()
        {
            for (int armyIndex = 0; armyIndex < _armiesData.Count; armyIndex++)
            {
                for (int enemyIndex = 0; enemyIndex < _armiesData.Count; enemyIndex++)
                {
                    if (armyIndex != enemyIndex) // TODO improve logic and make it as a list
                    {
                        _armiesData[armyIndex].enemyArmyData = _armiesData[enemyIndex];
                    }
                }
            }
        }

        public Vector3 FindCenterOfArmies()
        {
            Vector3 center = Vector3.zero;
            int armiesCount = 0;

            foreach (var armyData in _armiesData)
            {
                List<BaseUnit> units = armyData.AllUnits;

                if (units != null && units.Count > 0)
                {
                    center += PositionFinder.FindCenterOf(units);
                    armiesCount++;
                }
            }

            return center / armiesCount;
        }

        public bool IsAnyArmyWithUnits()
        {
            bool result = false;

            foreach (var armyData in _armiesData)
            {
                if (armyData.AllUnits.Count > 0)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
    }
}

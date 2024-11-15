using System;
using System.Collections.Generic;
using System.Linq;
using MassBattle.Core.Providers;
using MassBattle.Logic.Units;
using MassBattle.Logic.Utilities;
using UnityEngine;

namespace MassBattle.Logic.Armies
{
    public class ArmyProvider : IArmyProvider
    {
        public event Action<ArmyData> OnLastArmyStay = delegate { };
        public event Action OnNoArmyStay = delegate { };

        private readonly IUpdateProvider _updateProvider;
        private readonly List<ArmyData> _armiesData = new();

        public ArmyProvider(IUpdateProvider updateProvider)
        {
            _updateProvider = updateProvider;

            AttachToEvents();
        }

        private void AttachToEvents()
        {
            _updateProvider.OnEarlyUpdate += EarlyUpdateArmies;
            _updateProvider.OnUpdate += UpdateArmies;
        }

        private void EarlyUpdateArmies()
        {
            foreach (var armyData in _armiesData)
            {
                armyData.EarlyUpdateArmy();
            }
        }

        private void UpdateArmies()
        {
            foreach (var armyData in _armiesData)
            {
                armyData.UpdateArmy();
            }
        }

        ~ArmyProvider()
        {
            DetachFromEvents();
            ClearArmies();
        }

        private void DetachFromEvents()
        {
            if (_updateProvider != null)
            {
                _updateProvider.OnEarlyUpdate -= EarlyUpdateArmies;
                _updateProvider.OnUpdate -= UpdateArmies;
            }
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
            return _armiesData.FirstOrDefault(army => army.Id == armyId);
        }

        public void InitializedRegisteredArmies()
        {
            FillUpEnemiesForRegisteredArmies();
            TryNotifyBattleResult();
        }

        private void FillUpEnemiesForRegisteredArmies()
        {
            for (int i = 0; i < _armiesData.Count; i++)
            {
                ArmyData currentArmy = _armiesData[i];
                List<ArmyData> enemies = FindEnemiesForArmyId(currentArmy.Id);

                currentArmy.InjectEnemyArmies(enemies);
            }
        }

        private List<ArmyData> FindEnemiesForArmyId(string armyId)
        {
            return _armiesData.FindAll(army => army.Id != armyId);
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

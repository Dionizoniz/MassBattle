using System;
using UnityEngine;

namespace MassBattle.Logic.Armies
{
    public interface IArmyProvider
    {
        event Action<ArmyData> OnLastArmyStay;
        event Action OnNoArmyStay;

        void ClearArmies();
        void RegisterArmy(ArmyData armyData);
        ArmyData FindArmyBy(string armyId);
        void FillUpEnemiesForRegisteredArmies();
        Vector3 FindCenterOfArmies();
    }
}

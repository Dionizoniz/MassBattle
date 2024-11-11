using UnityEngine;

namespace MassBattle.Logic.Armies
{
    public interface IArmyProvider
    {
        void ClearArmies();
        void RegisterArmy(ArmyData armyData);
        ArmyData FindArmyBy(string armyId);
        void FillUpEnemiesForRegisteredArmies();
        Vector3 FindCenterOfArmies();
    }
}

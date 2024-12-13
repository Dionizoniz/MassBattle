using System.Collections.Generic;
using MassBattle.Core.Entities;
using UnityEngine;

namespace MassBattle.Logic.Databases.Armies
{
    public interface IBattleSetup : ICheckSetup
    {
        List<InitialArmyData> SavedArmiesData { get; }

        Vector2 UnitStackSizeRange { get; }

        void ClearSavedArmiesData();
        void SaveArmyData(InitialArmyData armyData);
    }
}

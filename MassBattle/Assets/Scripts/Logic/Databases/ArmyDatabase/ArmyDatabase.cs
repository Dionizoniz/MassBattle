using MassBattle.Core.Entities.Database;
using UnityEngine;

namespace MassBattle.Logic.Databases.ArmyDatabase
{
    [CreateAssetMenu(menuName = "Databases/" + nameof(ArmyDatabase), fileName = nameof(ArmyDatabase), order = 0)]
    public class ArmyDatabase : Database<InitialArmyData>, IArmyDatabase
    {
    }
}

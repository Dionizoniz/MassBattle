using MassBattle.Core.Entities.Database;
using UnityEngine;

namespace MassBattle.Logic.Databases.UnitDatabase
{
    [CreateAssetMenu(menuName = "Databases/" + nameof(UnitDatabase), fileName = nameof(UnitDatabase), order = 0)]
    public class UnitDatabase : Database<UnitData>, IUnitDatabase
    {
    }
}

using MassBattle.Core.Entities.Database;
using UnityEngine;

namespace MassBattle.Logic.Databases.ArmyDatabase
{
    [CreateAssetMenu(menuName = "Databases/" + nameof(ArmyDatabase), fileName = nameof(ArmyDatabase), order = 0)]
    public class ArmyDatabase : Database<InitialArmyData>, IArmyDatabase
    {
        public override bool IsSetupCorrect()
        {
            bool isSetupCorrect = base.IsSetupCorrect();

            foreach (var initialArmyData in _elements)
            {
                isSetupCorrect &= initialArmyData.IsSetupCorrect();
            }

            return isSetupCorrect;
        }
    }
}

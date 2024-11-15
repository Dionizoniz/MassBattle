using System.Collections.Generic;
using MassBattle.Core.Entities.Database;
using UnityEngine;

namespace MassBattle.Logic.Databases.ArmyDatabase
{
    [CreateAssetMenu(menuName = "Databases/" + nameof(ArmyDatabase), fileName = nameof(ArmyDatabase), order = 0)]
    public class ArmyDatabase : Database<InitialArmyData>, IArmyDatabase
    {
        // INFO: Turn into auto-property if you do not need save data between runs in editor mode
        private List<ArmyData> _savedArmiesData = new();

        public List<ArmyData> SavedArmiesData => _savedArmiesData;

        public void SaveArmyData(ArmyData armyData)
        {
            int index = FindSavedArmyDataIndexBy(armyData.Id);

            if (index >= 0)
            {
                _savedArmiesData.RemoveAt(index);
            }

            _savedArmiesData.Add(armyData);
        }

        private int FindSavedArmyDataIndexBy(string id)
        {
            return _savedArmiesData.FindIndex(armyData => armyData.Id == id);
        }

        public void ClearSavedArmiesData()
        {
            _savedArmiesData.Clear();
        }

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

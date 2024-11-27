using System.Collections.Generic;
using MassBattle.Core.Databases;
using UnityEngine;

namespace MassBattle.Logic.Databases.ArmyDatabase
{
    [CreateAssetMenu(menuName = "Databases/" + nameof(ArmyDatabase), fileName = nameof(ArmyDatabase), order = 0)]
    public class ArmyDatabase : Database<InitialArmyData>, IArmyDatabase
    {
        [Space, SerializeField]
        private int _minUnitStackSize;
        [SerializeField]
        private int _maxUnitStackSize = 150;

        private readonly List<InitialArmyData> _savedArmiesData = new();

        private bool UseSavedArmiesData => _savedArmiesData != null && _savedArmiesData.Count > 0;
        public List<InitialArmyData> ArmiesData => UseSavedArmiesData ? _savedArmiesData : _elements;

        public int MinUnitStackSize => _minUnitStackSize;
        public int MaxUnitStackSize => _maxUnitStackSize;

        public void SaveArmyData(InitialArmyData armyData)
        {
            int index = FindSavedArmyDataIndexBy(armyData.DescriptorId);

            if (index >= 0)
            {
                _savedArmiesData.RemoveAt(index);
            }

            _savedArmiesData.Add(armyData);
        }

        private int FindSavedArmyDataIndexBy(string id)
        {
            return _savedArmiesData.FindIndex(armyData => armyData.DescriptorId == id);
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

                int unitStackSize = initialArmyData.DefaultUnitStackSize;
                isSetupCorrect &= unitStackSize >= MinUnitStackSize && unitStackSize <= MaxUnitStackSize;
            }

            return isSetupCorrect;
        }
    }
}

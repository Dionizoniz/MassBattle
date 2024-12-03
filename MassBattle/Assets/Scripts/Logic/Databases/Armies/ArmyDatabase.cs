using System.Collections.Generic;
using MassBattle.Core.Databases;
using MassBattle.Core.Engine;
using MassBattle.Logic.Databases.Colors;
using UnityEngine;

namespace MassBattle.Logic.Databases.Armies
{
    [CreateAssetMenu(menuName = "Databases/" + nameof(ArmyDatabase), fileName = nameof(ArmyDatabase), order = -10000)]
    public class ArmyDatabase : ExtendedScriptableObject, IArmyDatabase
    {
        [SerializeField]
        protected List<InitialArmyData> _elements = new();
        [Space, SerializeField]
        private int _minUnitStackSize;
        [SerializeField]
        private int _maxUnitStackSize = 150;

        private readonly List<InitialArmyData> _savedArmiesData = new();
        private bool UseSavedArmiesData => _savedArmiesData != null && _savedArmiesData.Count > 0;
        public List<InitialArmyData> ArmiesData => UseSavedArmiesData ? _savedArmiesData : _elements;

        public Vector2 UnitStackSizeRange => new(_minUnitStackSize, _maxUnitStackSize);

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

        public bool IsSetupCorrect()
        {
            // bool isSetupCorrect = base.IsSetupCorrect();
            //
            // foreach (var initialArmyData in _descriptors)
            // {
            //     //    isSetupCorrect &= initialArmyData.IsSetupCorrect(); TODO restore code
            //
            //     //    int unitStackSize = initialArmyData.DefaultUnitStackSize;
            //     //    isSetupCorrect &= unitStackSize >= MinUnitStackSize && unitStackSize <= MaxUnitStackSize;
            // }

            return true;
        }

        public IEnumerable<string> FindDescriptorIds() => throw new System.NotImplementedException();
        public ColorDescriptor TryFindNextElementFor(int index) => throw new System.NotImplementedException();
        public ColorDescriptor TryFindElementBy(string descriptorId) => throw new System.NotImplementedException();
        public ColorDescriptor FindDefaultElement() => throw new System.NotImplementedException();
    }
}

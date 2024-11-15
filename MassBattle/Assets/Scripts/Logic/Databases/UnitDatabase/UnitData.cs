using System;
using MassBattle.Core.Entities.Database;
using MassBattle.Logic.Units;
using UnityEngine;

namespace MassBattle.Logic.Databases.UnitDatabase
{
    [Serializable]
    public class UnitData : BaseData
    {
        [SerializeField]
        private string _unitName;
        [SerializeField]
        private BaseUnit _unitPrefabToSpawn;

        public string UnitName => _unitName;
        public BaseUnit UnitPrefabToSpawn => _unitPrefabToSpawn;

        protected override string ClassName => nameof(UnitData);
    }
}

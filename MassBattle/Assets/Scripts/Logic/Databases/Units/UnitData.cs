using System;
using MassBattle.Core.Databases;
using MassBattle.Core.Entities;
using MassBattle.Logic.Units;
using UnityEngine;

namespace MassBattle.Logic.Databases.Units
{
    [Serializable]
    public class UnitData : BaseData, IName
    {
        [SerializeField]
        private string _unitName;
        [SerializeField]
        private BaseUnit _unitPrefabToSpawn;

        public string Name => _unitName;
        public BaseUnit UnitPrefabToSpawn => _unitPrefabToSpawn;

        protected override string ClassName => nameof(UnitData);
    }
}

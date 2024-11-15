using System;
using MassBattle.Core.Utilities;
using UnityEngine;

namespace MassBattle.Core.Entities.Database
{
    [Serializable]
    public abstract class BaseData : IId
    {
        [SerializeField, ReadOnly]
        private string _id;

        public string Id => _id;
        protected abstract string ClassName { get; }

        public void GenerateId(int index)
        {
            _id = $"{ClassName}{index}";
        }

        protected void OverrideId(string newId)
        {
            _id = newId;
        }
    }
}

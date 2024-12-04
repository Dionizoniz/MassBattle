using System;
using MassBattle.Core.Entities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MassBattle.Core.Databases
{
    [Serializable]
    public abstract class BaseData : IDescriptorId
    {
        [SerializeField, ReadOnly]
        private string _id;

        public string DescriptorId => _id;
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

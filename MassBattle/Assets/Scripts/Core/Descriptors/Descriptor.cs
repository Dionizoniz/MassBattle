using System;
using MassBattle.Core.Engine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MassBattle.Core.Descriptors
{
    public abstract class Descriptor : ExtendedScriptableObject, IDescriptor
    {
        [SerializeField, ReadOnly]
        private string _descriptorId;

        public string DescriptorId => _descriptorId;

        private void Awake()
        {
            TryGenerateDescriptorId();
        }

        private void TryGenerateDescriptorId()
        {
            if (string.IsNullOrEmpty(_descriptorId))
            {
                _descriptorId = Guid.NewGuid().ToString();
            }
        }
    }
}

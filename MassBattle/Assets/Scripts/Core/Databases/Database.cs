using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using MassBattle.Core.Descriptors;
using MassBattle.Core.Entities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MassBattle.Core.Databases
{
    public abstract class Database<T> : BaseDatabase, IDatabase<T> where T : IDescriptor
    {
        [SerializeField]
        protected List<T> _descriptors = new();

        public ValueDropdownList<string> PossibleDescriptorsDropdown => CreateIdDropdown();

        private ValueDropdownList<string> CreateIdDropdown()
        {
            var descriptorsNames = new ValueDropdownList<string>();
            var sortedDescriptors = _descriptors.OrderBy(descriptor => descriptor.AssetName);

            foreach (var descriptor in sortedDescriptors)
            {
                descriptorsNames.Add(descriptor.AssetName, descriptor.DescriptorId);
            }

            return descriptorsNames;
        }

        public IEnumerable<string> FindDescriptorIds()
        {
            return _descriptors.Select(element => element.DescriptorId);
        }

        public T TryFindNextElementFor(int index)
        {
            index++;
            index %= _descriptors.Count;

            return _descriptors[index];
        }

        public T TryFindElementBy(string descriptorId)
        {
            T result = _descriptors.FirstOrDefault(element => element.DescriptorId == descriptorId);

            if (result == null)
            {
                Debug.LogWarning("Database has not contain ID. Returning the first element.");
                result = FindDefaultElement();
            }

            return result;
        }

        public T FindDefaultElement()
        {
            return _descriptors[0];
        }

        public override bool IsSetupCorrect()
        {
            bool isCorrectSetup = true;

            foreach (T descriptor in _descriptors)
            {
                isCorrectSetup &= descriptor.IsSetupCorrect();
            }

            return isCorrectSetup && IsNoDuplicatedIds();
        }

        private bool IsNoDuplicatedIds()
        {
            List<string> duplicateIds = _descriptors.GroupBy(element => element.DescriptorId)
                                                    .Where(group => group.Count() > 1)
                                                    .Select(group => group.Key)
                                                    .ToList();

            return duplicateIds.Count == 0;
        }
    }
}

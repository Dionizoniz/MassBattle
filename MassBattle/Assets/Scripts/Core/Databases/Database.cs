using System.Collections.Generic;
using System.Linq;
using MassBattle.Core.Descriptors;
using MassBattle.Core.Entities;
using UnityEngine;

namespace MassBattle.Core.Databases
{
    public abstract class Database<T> : BaseDatabase, IDatabase<T> where T : IDescriptor
    {
        [SerializeField]
        protected List<T> _elements = new();

        public IEnumerable<string> FindDescriptorIds()
        {
            return _elements.Select(element => element.DescriptorId);
        }

        public T TryFindNextElementFor(int index)
        {
            index++;
            index %= _elements.Count;

            return _elements[index];
        }

        public T TryFindElementBy(string descriptorId)
        {
            T result = _elements.FirstOrDefault(element => element.DescriptorId == descriptorId);

            if (result == null)
            {
                Debug.LogWarning("Database has not contain ID. Returning the first element.");
                result = FindDefaultElement();
            }

            return result;
        }

        public T FindDefaultElement()
        {
            return _elements[0];
        }

        public override bool IsSetupCorrect()
        {
            List<string> duplicateIds = _elements.GroupBy(element => element.DescriptorId)
                                                 .Where(group => group.Count() > 1)
                                                 .Select(group => group.Key)
                                                 .ToList();

            return duplicateIds.Count == 0;
        }
    }
}

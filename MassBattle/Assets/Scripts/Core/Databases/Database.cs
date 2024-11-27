using System.Collections.Generic;
using System.Linq;
using MassBattle.Core.Entities;
using UnityEngine;

namespace MassBattle.Core.Databases
{
    public abstract class Database<T> : BaseDatabase, IDatabase<T> where T : IDescriptorId
    {
        [SerializeField]
        protected List<T> _elements = new();

        public IEnumerable<string> FindAllDescriptorIds()
        {
            return _elements.Select(element => element.DescriptorId);
        }

        public T TryFindElementBy(int index)
        {
            T result;

            if (index >= 0 && index < _elements.Count)
            {
                result = _elements[index];
            }
            else
            {
                Debug.LogWarning("Database index out of range. Returning first element.");
                result = FindDefaultElement();
            }

            return result;
        }

        public T FindDefaultElement()
        {
            return _elements[0];
        }

        public T TryFindNextElementFor(int index)
        {
            index++;
            index %= _elements.Count;

            return _elements[index];
        }

        public T TryFindElementBy(string id)
        {
            T result = _elements.FirstOrDefault(armySetup => armySetup.DescriptorId == id);

            if (result == null)
            {
                Debug.LogWarning("Database has not contain ID. Returning first element.");
                result = FindDefaultElement();
            }

            return result;
        }

        private void OnValidate()
        {
            GenerateIds();
        }

        private void GenerateIds()
        {
            for (var i = 0; i < _elements.Count; i++)
            {
                //    _elements[i].GenerateId(i); TODO Improve logic
            }
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

using System.Collections.Generic;
using System.Linq;
using MassBattle.Core.Entities;
using UnityEngine;

namespace MassBattle.Logic.Databases
{
    public class BaseDatabase<T> : ScriptableObject where T : IId
    {
        [SerializeField]
        private List<T> _elements = new();

        public List<string> FindAllElementIds()
        {
            return _elements.Select(x => x.Id).ToList();
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
                result = FindFirstElement();
            }

            return result;
        }

        public T TryFindElementBy(string id)
        {
            T result = _elements.FirstOrDefault(armySetup => armySetup.Id == id);

            if (result == null)
            {
                Debug.LogWarning("Database has not contain ID. Returning first element.");
                result = FindFirstElement();
            }

            return result;
        }

        private T FindFirstElement()
        {
            return _elements[0];
        }
    }
}

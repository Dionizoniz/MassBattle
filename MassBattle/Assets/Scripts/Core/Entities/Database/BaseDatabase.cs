using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MassBattle.Core.Entities.Database
{
    public class BaseDatabase<T> : ScriptableObject, IBaseDatabase<T> where T : IId
    {
        [SerializeField]
        protected List<T> _elements = new();

        public List<string> FindAllElementIds()
        {
            return _elements.Select(element => element.Id).ToList();
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
            T result = _elements.FirstOrDefault(armySetup => armySetup.Id == id);

            if (result == null)
            {
                Debug.LogWarning("Database has not contain ID. Returning first element.");
                result = FindDefaultElement();
            }

            return result;
        }
    }
}

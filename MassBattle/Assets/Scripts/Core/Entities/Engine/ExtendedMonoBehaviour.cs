using UnityEngine;

namespace MassBattle.Core.Entities.Engine
{
    public class ExtendedMonoBehaviour : MonoBehaviour
    {
        private GameObject _gameObject;
        private Transform _transform;

        private void Awake()
        {
            CacheReferences();
        }

        private void CacheReferences()
        {
            _gameObject = gameObject;
            _transform = transform;
        }
    }
}

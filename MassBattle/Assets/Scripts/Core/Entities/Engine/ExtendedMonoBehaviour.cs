using UnityEngine;

namespace MassBattle.Core.Entities.Engine
{
    public class ExtendedMonoBehaviour : MonoBehaviour
    {
        public GameObject _gameObject;
        public Transform _transform;

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

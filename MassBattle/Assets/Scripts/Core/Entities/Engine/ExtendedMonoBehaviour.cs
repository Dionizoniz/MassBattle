using UnityEngine;

namespace MassBattle.Core.Entities.Engine
{
    public class ExtendedMonoBehaviour : MonoBehaviour
    {
        [HideInInspector]
        public GameObject _gameObject;
        [HideInInspector]
        public Transform _transform;
        [HideInInspector]
        public Vector3 cachedPosition;

        public void CachePosition()
        {
            cachedPosition = _transform.position;
        }

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

using System.Collections.Generic;
using MassBattle.Logic.Units.Weapons;
using UnityEngine;

namespace MassBattle.Logic.Providers
{
    public class UnitsFactory : IUnitsFactory
    {
        private const string ARROWS_ROOT_NAME = "ArrowsRoot";

        private Transform _arrowsRoot;
        private readonly Stack<Arrow> _arrowsPool = new();

        public UnitsFactory()
        {
            CreateArrowsRoot();
        }

        private void CreateArrowsRoot()
        {
            _arrowsRoot = new GameObject(ARROWS_ROOT_NAME).transform;
        }

        public Arrow CreateArrowInstance(Arrow arrowPrefab)
        {
            Arrow arrow = _arrowsPool.Count > 0 ? _arrowsPool.Pop() : Object.Instantiate(arrowPrefab, _arrowsRoot);
            arrow._gameObject.SetActive(true);

            return arrow;
        }

        public void ReturnArrowInstance(Arrow arrow)
        {
            arrow._gameObject.SetActive(false);
            _arrowsPool.Push(arrow);
        }
    }
}

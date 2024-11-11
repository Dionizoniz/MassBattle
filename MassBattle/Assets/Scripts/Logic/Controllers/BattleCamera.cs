using System;
using MassBattle.Core.Entities.Engine;
using MassBattle.Logic.Armies;
using MassBattle.Logic.Providers;
using MassBattle.Logic.Utilities;
using UnityEngine;

namespace MassBattle.Logic.Controllers
{
    public class BattleCamera : ExtendedMonoBehaviour, IBattleCamera
    {
        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private float _adjustPositionSpeed = 2f;

        private IArmyProvider _armyProvider;
        private IUpdateProvider _updateProvider;

        public void Initialize(IArmyProvider armyProvider, IUpdateProvider updateProvider)
        {
            _armyProvider = armyProvider;
            _updateProvider = updateProvider;

            _updateProvider.OnUpdate += UpdateCameraTransform;
        }

        private void UpdateCameraTransform()
        {
            Transform cameraTransform = _camera.transform;
            Vector3 armiesCenter = _armyProvider.FindCenterOfArmies();
            Vector3 newForward = armiesCenter - cameraTransform.position;

            cameraTransform.forward = newForward; // TODO lerp
        }

        private void OnDestroy()
        {
            if (_updateProvider != null)
            {
                _updateProvider.OnUpdate -= UpdateCameraTransform;
            }
        }
    }
}

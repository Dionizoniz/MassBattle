using MassBattle.Core.Entities.Engine;
using MassBattle.Core.Providers;
using MassBattle.Logic.Armies;
using MassBattle.Logic.Providers;
using UnityEngine;

namespace MassBattle.Logic.Controllers
{
    public class BattleCamera : ExtendedMonoBehaviour, IBattleCamera
    {
        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private float _adjustPositionSpeed = 0.1f;

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
            if (_armyProvider.IsAnyArmyWithUnits())
            {
                Transform cameraTransform = _camera.transform;
                Vector3 armiesCenter = _armyProvider.FindCenterOfArmies();
                Vector3 newForward = armiesCenter - cameraTransform.position;

                float speed = _adjustPositionSpeed * Time.deltaTime;
                cameraTransform.forward = Vector3.Lerp(cameraTransform.forward, newForward, speed);
            }
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

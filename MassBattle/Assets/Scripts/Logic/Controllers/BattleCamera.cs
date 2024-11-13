using MassBattle.Core.Entities.Engine;
using MassBattle.Core.Providers;
using MassBattle.Core.UserInput;
using MassBattle.Logic.Armies;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace MassBattle.Logic.Controllers
{
    public class BattleCamera : ExtendedMonoBehaviour, IBattleCamera
    {
        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private float _adjustPositionSpeed = 0.1f;

        [Space, SerializeField]
        private Vector3 _minZoomLevel = new(0f, -65f, -50f);
        [SerializeField]
        private Vector3 _maxZoomLevel = new(0f, 0f, 50f);
        [SerializeField]
        private float _zoomSpeedMultiplier = 10f;

        private IArmyProvider _armyProvider;
        private IUpdateProvider _updateProvider;
        private IInputFacade _inputFacade;

        public void Initialize(IArmyProvider armyProvider, IUpdateProvider updateProvider, IInputFacade inputFacade)
        {
            _armyProvider = armyProvider;
            _updateProvider = updateProvider;
            _inputFacade = inputFacade;

            _updateProvider.OnUpdate += UpdateCameraTransform;
            _inputFacade.OnMouseWheelScroll += AdjustCameraOffset;
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

        private void AdjustCameraOffset(float scrollOffset)
        {
            Vector3 targetZoomLevel = scrollOffset > 0 ? _maxZoomLevel : _minZoomLevel;
            float speed = _zoomSpeedMultiplier * Time.deltaTime;

            _transform.localPosition = Vector3.MoveTowards(_transform.localPosition, targetZoomLevel, speed);
        }

        private void OnDestroy()
        {
            if (_updateProvider != null)
            {
                _updateProvider.OnUpdate -= UpdateCameraTransform;
                _inputFacade.OnMouseWheelScroll -= AdjustCameraOffset;
            }
        }
    }
}

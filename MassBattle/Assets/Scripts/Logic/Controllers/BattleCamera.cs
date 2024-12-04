using MassBattle.Core.Engine;
using MassBattle.Core.Providers;
using MassBattle.Core.UserInput;
using MassBattle.Logic.Armies;
using UnityEngine;
using Zenject;

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

        [Space, SerializeField]
        private int _framesCountToRefreshArmiesCenter = 10;

        private IArmyProvider _armyProvider;
        private IUpdateProvider _updateProvider;
        private IInputFacade _inputFacade;

        private Vector3 _cachedArmiesCenter;
        private int _framesToRefreshArmiesCenterLeft;

        [Inject]
        private void Construct(IArmyProvider armyProvider, IUpdateProvider updateProvider, IInputFacade inputFacade)
        {
            _armyProvider = armyProvider;
            _updateProvider = updateProvider;
            _inputFacade = inputFacade;

            AttachToEvents();
        }

        private void AttachToEvents()
        {
            _updateProvider.OnUpdate += UpdateCameraTransform;
            _inputFacade.OnMouseWheelScroll += AdjustCameraOffset;
        }

        private void UpdateCameraTransform()
        {
            Transform cameraTransform = _camera.transform;
            Vector3 armiesCenter = FindArmiesCenter();
            Vector3 newForward = armiesCenter - cameraTransform.position;

            float speed = _adjustPositionSpeed * Time.deltaTime;
            cameraTransform.forward = Vector3.Lerp(cameraTransform.forward, newForward, speed);
        }

        private Vector3 FindArmiesCenter()
        {
            _framesToRefreshArmiesCenterLeft--;

            if (_framesToRefreshArmiesCenterLeft <= 0)
            {
                if (_armyProvider.IsAnyArmyWithUnits())
                {
                    _cachedArmiesCenter = _armyProvider.FindCenterOfArmies();
                }

                _framesToRefreshArmiesCenterLeft = _framesCountToRefreshArmiesCenter;
            }

            return _cachedArmiesCenter;
        }

        private void AdjustCameraOffset(float scrollOffset)
        {
            Vector3 targetZoomLevel = scrollOffset > 0 ? _maxZoomLevel : _minZoomLevel;
            float speed = _zoomSpeedMultiplier * Time.deltaTime;

            _transform.localPosition = Vector3.MoveTowards(_transform.localPosition, targetZoomLevel, speed);
        }

        private void OnDestroy()
        {
            DetachFromEvents();
        }

        private void DetachFromEvents()
        {
            if (_updateProvider != null)
            {
                _updateProvider.OnUpdate -= UpdateCameraTransform;
            }

            if (_inputFacade != null)
            {
                _inputFacade.OnMouseWheelScroll -= AdjustCameraOffset;
            }
        }
    }
}

using System;
using MassBattle.Core.Entities.Engine;
using MassBattle.Core.Providers;
using UnityEngine;

namespace MassBattle.Core.UserInput
{
    public class InputFacade : ExtendedMonoBehaviour, IInputFacade
    {
        public event Action<float> OnMouseWheelScroll = delegate { };
        public event Action OnExitButton = delegate { };

        [SerializeField]
        private KeyCode _exitButton = KeyCode.Escape;

        private IUpdateProvider _updateProvider;

        public void Initialize(IUpdateProvider updateProvider)
        {
            _updateProvider = updateProvider;

            AttachToEvents();
        }

        private void AttachToEvents()
        {
            _updateProvider.OnUpdate += ProcessInput;
        }

        private void ProcessInput()
        {
            ProcessMouseWheelScroll();
            ProcessExitButton();
        }

        private void ProcessMouseWheelScroll()
        {
            float mouseWheelOffset = Input.mouseScrollDelta.y;

            if (mouseWheelOffset != 0)
            {
                OnMouseWheelScroll.Invoke(mouseWheelOffset);
            }
        }

        private void ProcessExitButton()
        {
            if (Input.GetKeyDown(_exitButton))
            {
                OnExitButton.Invoke();
            }
        }

        private void OnDestroy()
        {
            DetachFromEvents();
        }

        private void DetachFromEvents()
        {
            _updateProvider.OnUpdate -= ProcessInput;
        }
    }
}

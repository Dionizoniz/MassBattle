﻿using System;
using MassBattle.Core.Engine;
using MassBattle.Core.Providers;
using UnityEngine;
using Zenject;

namespace MassBattle.Core.UserInput
{
    public class InputFacade : ExtendedMonoBehaviour, IInputFacade
    {
        public event Action<float> OnMouseWheelScroll = delegate { };
        public event Action OnExitButton = delegate { };

        [SerializeField]
        private KeyCode _exitButton = KeyCode.Escape;

        private IUpdateProvider _updateProvider;

        [Inject]
        private void Construct(IUpdateProvider updateProvider)
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
            if (_updateProvider != null)
            {
                _updateProvider.OnUpdate -= ProcessInput;
            }
        }
    }
}

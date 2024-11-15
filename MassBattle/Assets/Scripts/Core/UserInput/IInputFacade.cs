using System;
using MassBattle.Core.Providers;

namespace MassBattle.Core.UserInput
{
    public interface IInputFacade
    {
        event Action<float> OnMouseWheelScroll;
        event Action OnExitButton;

        void InjectData(IUpdateProvider updateProvider);
    }
}

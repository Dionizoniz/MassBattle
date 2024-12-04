using System;

namespace MassBattle.Core.UserInput
{
    public interface IInputFacade
    {
        event Action<float> OnMouseWheelScroll;
        event Action OnExitButton;
    }
}

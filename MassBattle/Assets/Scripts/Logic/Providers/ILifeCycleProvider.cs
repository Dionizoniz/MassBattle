using System;

namespace MassBattle.Logic.Providers
{
    public interface ILifeCycleProvider
    {
        event Action OnUpdate;
    }
}

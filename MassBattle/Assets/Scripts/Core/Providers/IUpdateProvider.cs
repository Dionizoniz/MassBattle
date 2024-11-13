using System;

namespace MassBattle.Core.Providers
{
    public interface IUpdateProvider
    {
        event Action OnEarlyUpdate;
        event Action OnUpdate;
    }
}

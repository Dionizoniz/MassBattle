using System;

namespace MassBattle.Logic.Providers
{
    public interface IUpdateProvider
    {
        event Action OnUpdate;
    }
}

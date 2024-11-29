using System;

namespace MassBattle.Core.Installers
{
    public interface IInstaller
    {
        event Action OnSpawnFinish;
    }
}

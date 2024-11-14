using System;

namespace MassBattle.Core.Entities.Installers
{
    public interface IInstaller
    {
        event Action OnSpawnFinish;
    }
}

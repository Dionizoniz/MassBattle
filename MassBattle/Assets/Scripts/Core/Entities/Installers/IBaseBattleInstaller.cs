using System;

namespace MassBattle.Core.Entities.Installers
{
    public interface IBaseBattleInstaller
    {
        event Action OnSpawnFinish;
    }
}

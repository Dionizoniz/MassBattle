using System;

namespace MassBattle.Core.Entities
{
    public interface ISceneSpawner
    {
        event Action OnSpawnScene;

        bool IsSceneSpawned { get; }
    }
}

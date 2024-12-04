using MassBattle.Core.Installers;
using MassBattle.Logic.BattleCreator;
using UnityEngine;

namespace MassBattle.DependencyInjection.Installers.Systems
{
    public class BattleSpawnerInstaller : ExtendedMonoInstaller
    {
        [SerializeField]
        private BattleSpawner _battleSpawner;

        public override void InstallBindings()
        {
            BindFromComponentInNewPrefab(_battleSpawner);
        }
    }
}

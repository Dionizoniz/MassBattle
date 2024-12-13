using MassBattle.Core.Installers;
using MassBattle.Logic.Battle.Spawner;
using UnityEngine;

namespace MassBattle.DependencyInjection.Installers.Systems
{
    public class ArmySpawnerInstaller : ExtendedMonoInstaller
    {
        [SerializeField]
        private ArmySpawner _armySpawner;

        public override void InstallBindings()
        {
            BindInterfacesToFromComponentInNewPrefab(_armySpawner);
        }

        public override bool IsSetupCorrect() => _armySpawner != null;
    }
}

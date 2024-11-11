using MassBattle.Core.Entities.Engine;
using MassBattle.Logic.Armies;
using MassBattle.Logic.BattleCreator;
using MassBattle.Logic.Providers;
using MassBattle.Logic.Setup;
using UnityEngine;

namespace MassBattle.Logic.Installers
{
    public class BattleInstaller : ExtendedMonoBehaviour, IBattleInstaller
    {
        [SerializeField]
        private BattleSpawner _battleSpawnerToSpawn;
        [SerializeField]
        private LifeCycleProvider _lifeCycleProviderToSpawn;

        [Space, SerializeField]
        private BattleSetup _battleSetup;

        private IBattleSpawner _battleSpawner;
        private IArmyProvider _armyProvider;
        private ILifeCycleProvider _lifeCycleProvider;

        private void Awake()
        {
            _battleSpawner = Instantiate(_battleSpawnerToSpawn);
            _lifeCycleProvider = Instantiate(_lifeCycleProviderToSpawn);

            _armyProvider = new ArmyProvider();
        }

        private void InstantiateElementsToSpawn()
        {
            _battleSpawner = Instantiate(_battleSpawnerToSpawn);
            _lifeCycleProvider = Instantiate(_lifeCycleProviderToSpawn);
        }

        private void Start()
        {
            _battleSpawner.Initialize(_battleSetup, _armyProvider, _lifeCycleProvider);
        }
    }
}

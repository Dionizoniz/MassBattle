using MassBattle.Core.Entities.Engine;
using MassBattle.Logic.Armies;
using MassBattle.Logic.BattleCreator;
using MassBattle.Logic.Providers;
using UnityEngine;

namespace MassBattle.Logic.Installers
{
    public class BattleInstaller : ExtendedMonoBehaviour, IBattleInstaller
    {
        [SerializeField]
        private BattleSpawner _battleSpawnerToSpawn;
        [SerializeField]
        private UpdateProvider _updateProviderToSpawn;

        [Space, SerializeField]
        private BattleSetup _battleSetup;

        private IBattleSpawner _battleSpawner;
        private IArmyProvider _armyProvider;
        private IUpdateProvider _updateProvider;

        private void Awake()
        {
            InstantiateElementsToSpawn();
            CreateInstances();
        }

        private void InstantiateElementsToSpawn()
        {
            _battleSpawner = Instantiate(_battleSpawnerToSpawn);
            _updateProvider = Instantiate(_updateProviderToSpawn);
        }

        private void CreateInstances()
        {
            _armyProvider = new ArmyProvider();
        }

        private void Start()
        {
            _battleSpawner.Initialize(_battleSetup, _armyProvider, _updateProvider);
        }
    }
}

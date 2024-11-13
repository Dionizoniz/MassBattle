using MassBattle.Core.Entities.Engine;
using MassBattle.Core.Providers;
using MassBattle.Logic.Armies;
using MassBattle.Logic.BattleCreator;
using MassBattle.Logic.Controllers;
using MassBattle.Logic.Databases;
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
        [SerializeField]
        private BattleCamera _battleCameraToSpawn;
        [SerializeField]
        private Transform _cameraControllerRoot;

        [Space, SerializeField]
        private BattleSetup _battleSetup;
        [SerializeField]
        private ColorDatabase _colorDatabase;

        public IArmyProvider ArmyProvider { get; private set; }

        private IBattleSpawner _battleSpawner;
        private IUpdateProvider _updateProvider;
        private IBattleCamera _battleCamera;
        private IUnitsFactory _unitsFactory;

        private void Awake()
        {
            SpawnSystems();
            CreateInstances();
        }

        private void SpawnSystems()
        {
            _battleSpawner = Instantiate(_battleSpawnerToSpawn);
            _updateProvider = Instantiate(_updateProviderToSpawn);

            _battleCamera = Instantiate(_battleCameraToSpawn, _cameraControllerRoot);
            AdjustCameraControllerRoot();
        }

        private void AdjustCameraControllerRoot()
        {
            _cameraControllerRoot.transform.SetParent(null);
        }

        private void CreateInstances()
        {
            ArmyProvider = new ArmyProvider();
            _unitsFactory = new UnitsFactory();
        }

        private void Start()
        {
            InitializeSystems();
        }

        private void InitializeSystems()
        {
            _battleSpawner.Initialize(_battleSetup, ArmyProvider, _updateProvider, _unitsFactory, _colorDatabase);
            _battleCamera.Initialize(ArmyProvider, _updateProvider);
        }
    }
}

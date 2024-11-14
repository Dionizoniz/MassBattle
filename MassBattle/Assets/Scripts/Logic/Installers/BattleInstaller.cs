using MassBattle.Core.Entities.Installers;
using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Core.UserInput;
using MassBattle.Logic.Armies;
using MassBattle.Logic.BattleCreator;
using MassBattle.Logic.Controllers;
using MassBattle.Logic.Databases;
using MassBattle.Logic.Providers;
using UnityEngine;

namespace MassBattle.Logic.Installers
{
    public class BattleInstaller : BaseInstaller, IBattleInstaller
    {
        [SerializeField]
        private BattleSpawner _battleSpawnerToSpawn;
        [SerializeField]
        private UpdateProvider _updateProviderToSpawn;
        [SerializeField]
        private InputFacade _inputFacadeToSpawn;

        [Space, SerializeField]
        private Transform _cameraControllerRoot;
        [SerializeField]
        private BattleCamera _battleCameraToSpawn;

        [Space, SerializeField]
        private BattleSetup _battleSetup;
        [SerializeField]
        private ColorDatabase _colorDatabase;
        [SerializeField]
        private SceneLoader _sceneLoader;

        public IArmyProvider ArmyProvider { get; private set; }
        public IInputFacade InputFacade { get; private set; }
        public ISceneLoader SceneLoader => _sceneLoader;

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
            InputFacade = Instantiate(_inputFacadeToSpawn);

            _battleCamera = Instantiate(_battleCameraToSpawn, _cameraControllerRoot);
            AdjustCameraControllerRoot();
        }

        private void AdjustCameraControllerRoot()
        {
            _cameraControllerRoot.transform.SetParent(null);
        }

        private void CreateInstances()
        {
            ArmyProvider = new ArmyProvider(_updateProvider);
            _unitsFactory = new UnitsFactory();
        }

        private void Start()
        {
            InitializeSystems();
        }

        private void InitializeSystems()
        {
            _battleSpawner.Initialize(_battleSetup, ArmyProvider, _updateProvider, _unitsFactory, _colorDatabase,
                                      _sceneLoader);

            _battleCamera.Initialize(ArmyProvider, _updateProvider, InputFacade);
            InputFacade.Initialize(_updateProvider);
        }

        public override bool IsSetupCorrect()
        {
            bool isSetupCorrect = true;

            isSetupCorrect &= _battleSpawnerToSpawn != null;
            isSetupCorrect &= _updateProviderToSpawn != null;
            isSetupCorrect &= _inputFacadeToSpawn != null;

            isSetupCorrect &= _cameraControllerRoot != null;
            isSetupCorrect &= _battleCameraToSpawn != null;

            isSetupCorrect &= _battleSetup != null;
            isSetupCorrect &= _colorDatabase != null;
            isSetupCorrect &= _sceneLoader != null;

            return isSetupCorrect;
        }
    }
}

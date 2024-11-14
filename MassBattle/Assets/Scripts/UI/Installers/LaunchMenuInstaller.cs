using MassBattle.Core.Entities.Installers;
using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.BattleCreator;
using MassBattle.Logic.Databases;
using MassBattle.UI.LaunchMenu;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MassBattle.UI.Installers
{
    public class LaunchMenuInstaller : BaseInstaller, ILaunchMenuInstaller
    {
        [SerializeField]
        private LaunchMenuController _launchMenuControllerToSpawn;
        [SerializeField]
        private EventSystem _eventSystemToSpawn;

        [Space, SerializeField]
        private BattleSetup _battleSetup;
        [SerializeField]
        private ColorDatabase _colorDatabase;
        [SerializeField]
        private SceneLoader _sceneLoader;

        private ILaunchMenuController _launchMenuController;
        private IExitGameProvider _exitGameProvider;

        private void Awake()
        {
            SpawnLaunchMenuController();
            SpawnEventSystem();
            CreateInstances();
        }

        private void SpawnLaunchMenuController()
        {
            _launchMenuController = Instantiate(_launchMenuControllerToSpawn);
        }

        private void SpawnEventSystem()
        {
            Instantiate(_eventSystemToSpawn);
        }

        private void CreateInstances()
        {
            _exitGameProvider = new ExitGameProvider();
        }

        protected override void Start()
        {
            InjectData();

            base.Start();
        }

        private void InjectData()
        {
            _launchMenuController.InjectData(_battleSetup, _colorDatabase, _sceneLoader, _exitGameProvider);
        }

        public override bool IsSetupCorrect()
        {
            bool isSetupCorrect = true;

            isSetupCorrect &= _launchMenuControllerToSpawn != null;
            isSetupCorrect &= _eventSystemToSpawn != null;
            isSetupCorrect &= _battleSetup != null;
            isSetupCorrect &= _colorDatabase != null;
            isSetupCorrect &= _sceneLoader != null;

            return isSetupCorrect;
        }
    }
}

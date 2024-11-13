using MassBattle.Core.Entities.Installers;
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

        private void Awake()
        {
            SpawnLaunchMenuController();
            SpawnEventSystem();
        }

        private void SpawnLaunchMenuController()
        {
            _launchMenuController = Instantiate(_launchMenuControllerToSpawn);
        }

        private void SpawnEventSystem()
        {
            Instantiate(_eventSystemToSpawn);
        }

        private void Start()
        {
            InjectData();
        }

        private void InjectData()
        {
            _launchMenuController.InjectData(_battleSetup, _colorDatabase, _sceneLoader);
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

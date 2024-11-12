using MassBattle.Core.Entities.Engine;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.BattleCreator;
using MassBattle.Logic.Databases;
using MassBattle.UI.LaunchMenu;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MassBattle.UI.Installers
{
    public class LaunchMenuInstaller : ExtendedMonoBehaviour, ILaunchMenuInstaller
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
    }
}

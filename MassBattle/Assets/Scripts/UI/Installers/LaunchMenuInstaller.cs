using MassBattle.Core.Entities.Engine;
using MassBattle.Logic.Databases;
using MassBattle.Logic.Setup;
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

        private void Awake()
        {
            SpawnLaunchMenuController();
            SpawnEventSystem();
        }

        private void SpawnLaunchMenuController()
        {
            ILaunchMenuController launchMenu = Instantiate(_launchMenuControllerToSpawn);
            launchMenu.InjectData(_battleSetup, _colorDatabase);
        }

        private void SpawnEventSystem()
        {
            Instantiate(_eventSystemToSpawn);
        }
    }
}

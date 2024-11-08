using MassBattle.Logic.Setup;
using MassBattle.UI.LaunchMenu;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MassBattle.UI.Installers
{
    public class LaunchMenuInstaller : MonoBehaviour, ILaunchMenuInstaller
    {
        [SerializeField]
        private LaunchMenuController _launchMenuControllerToSpawn;
        [SerializeField]
        private EventSystem _eventSystemToSpawn;

        [Space, SerializeField]
        private BattleSetup _battleSetup;

        private void Awake()
        {
            SpawnLaunchMenuController();
            SpawnEventSystem();
        }

        private void SpawnLaunchMenuController()
        {
            ILaunchMenuController launchMenu = Instantiate(_launchMenuControllerToSpawn);
            launchMenu.InjectData(_battleSetup);
        }

        private void SpawnEventSystem()
        {
            Instantiate(_eventSystemToSpawn);
        }
    }
}

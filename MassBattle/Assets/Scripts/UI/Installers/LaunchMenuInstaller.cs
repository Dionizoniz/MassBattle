using MassBattle.Logic.Setup;
using MassBattle.UI.LaunchMenu;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MassBattle.UI.Installers
{
    public class LaunchMenuInstaller : MonoBehaviour, ILaunchMenuInstaller
    {
        [SerializeField]
        private LaunchMenuController _launchMenuController;
        [SerializeField]
        private BattleSetup _battleSetup;

        [Space, SerializeField]
        private EventSystem _eventSystem;

        private void Awake()
        {
            SpawnLaunchMenuController();
            SpawnEventSystem();
        }

        private void SpawnLaunchMenuController()
        {
            ILaunchMenuController launchMenu = Instantiate(_launchMenuController);
            launchMenu.InjectData(_battleSetup);
        }

        private void SpawnEventSystem()
        {
            Instantiate(_eventSystem);
        }
    }
}

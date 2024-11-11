using MassBattle.Core.Entities.Engine;
using MassBattle.Logic.Installers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MassBattle.UI.Installers
{
    public class BattleUiInstaller : ExtendedMonoBehaviour, IBattleUiInstaller
    {
        [SerializeField]
        private BattleInstaller _spawnedBattleInstaller;

        //[SerializeField]
        //private LaunchMenuController _launchMenuControllerToSpawn;
        [SerializeField]
        private EventSystem _eventSystemToSpawn;

        private void Awake()
        {
            //     SpawnLaunchMenuController();
            SpawnEventSystem();
        }

        // private void SpawnLaunchMenuController()
        // {
        //     ILaunchMenuController launchMenu = Instantiate(_launchMenuControllerToSpawn);
        //     launchMenu.InjectData(_battleSetup, _colorDatabase);
        // }

        private void SpawnEventSystem()
        {
            Instantiate(_eventSystemToSpawn);
        }
    }
}

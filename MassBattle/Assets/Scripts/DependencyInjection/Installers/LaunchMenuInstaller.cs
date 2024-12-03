using MassBattle.Core.Installers;
using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.Databases.Armies;
using MassBattle.Logic.Databases.Colors;
using MassBattle.Logic.Databases.Units;
using MassBattle.UI.LaunchMenu;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MassBattle.DependencyInjection.Installers
{
    public class LaunchMenuInstaller : ExtendedMonoInstaller
    {
        [SerializeField]
        private EventSystem _eventSystem;

        [SerializeField]
        private LaunchMenuController _launchMenuController;

        [Space, SerializeField]
        private ArmyDatabase _armyDatabase;
        [SerializeField]
        private ColorDatabase _colorDatabase;
        [SerializeField]
        private UnitDatabase _unitDatabase;
        [SerializeField]
        private SceneLoader _sceneLoader;

        public override void InstallBindings()
        {
            BindInterfacesTo<ExitGameProvider>();
            BindFromComponentInNewPrefab(_eventSystem);

            BindInterfacesToFromInstance(_armyDatabase);
            BindInterfacesToFromInstance(_colorDatabase);
            BindInterfacesToFromInstance(_unitDatabase);
            BindInterfacesToFromInstance(_sceneLoader);

            BindInterfacesToFromComponentInNewPrefab(_launchMenuController);
        }
    }
}

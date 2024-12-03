using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.Databases.Armies;
using MassBattle.Logic.Databases.Colors;
using MassBattle.Logic.Databases.Units;
using MassBattle.UI.LaunchMenu;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace MassBattle.DependencyInjection.Installers
{
    public class LaunchMenuInstaller : MonoInstaller
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
            Container.BindInterfacesTo<ExitGameProvider>().AsSingle().NonLazy();

            Container.Bind<EventSystem>().FromComponentInNewPrefab(_eventSystem).AsSingle().NonLazy();

            Container.BindInterfacesTo<LaunchMenuController>()
                     .FromComponentInNewPrefab(_launchMenuController)
                     .AsSingle()
                     .NonLazy();

            Container.BindInterfacesTo<ArmyDatabase>().FromInstance(_armyDatabase).AsSingle().NonLazy();
            Container.BindInterfacesTo<ColorDatabase>().FromInstance(_colorDatabase).AsSingle().NonLazy();
            Container.BindInterfacesTo<UnitDatabase>().FromInstance(_unitDatabase).AsSingle().NonLazy();
            Container.BindInterfacesTo<SceneLoader>().FromInstance(_sceneLoader).AsSingle().NonLazy();
        }
    }
}

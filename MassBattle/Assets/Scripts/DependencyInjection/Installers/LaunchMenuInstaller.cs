using MassBattle.Core.Providers;
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

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ExitGameProvider>().AsSingle().NonLazy(); // ???

            Container.Bind<EventSystem>().FromComponentInNewPrefab(_eventSystem).AsSingle().NonLazy();

            Container.BindInterfacesTo<LaunchMenuController>()
                     .FromComponentInNewPrefab(_launchMenuController)
                     .AsSingle()
                     .NonLazy();
        }
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace MassBattle.DependencyInjection.Installers
{
    public class LaunchMenuInstaller : MonoInstaller
    {
        [SerializeField]
        private EventSystem _eventSystem;

        public override void InstallBindings()
        {
            BindEventSystem();
        }

        private void BindEventSystem()
        {
            // INFO spawn and bind prefab
            Container.Bind<EventSystem>().FromComponentInNewPrefab(_eventSystem).AsSingle().NonLazy();
        }
    }
}

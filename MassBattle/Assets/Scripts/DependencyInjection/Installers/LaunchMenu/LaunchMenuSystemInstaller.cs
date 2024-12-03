using MassBattle.Core.Installers;
using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MassBattle.DependencyInjection.Installers.LaunchMenu
{
    public class LaunchMenuSystemInstaller : ExtendedMonoInstaller
    {
        [SerializeField]
        private EventSystem _eventSystem;
        [SerializeField]
        private SceneLoader _sceneLoader;

        public override void InstallBindings()
        {
            BindInterfacesTo<ExitGameProvider>();

            BindFromComponentInNewPrefab(_eventSystem);
            BindInterfacesToFromInstance(_sceneLoader);
        }
    }
}

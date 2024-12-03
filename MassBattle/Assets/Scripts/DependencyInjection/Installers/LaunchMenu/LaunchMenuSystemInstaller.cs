using MassBattle.Core.Installers;
using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.UI.FadePanel.Executors;
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
            Bind<FadeExecutor>();

            BindFromComponentInNewPrefab(_eventSystem);
            BindInterfacesToFromInstance(_sceneLoader);
        }
    }
}

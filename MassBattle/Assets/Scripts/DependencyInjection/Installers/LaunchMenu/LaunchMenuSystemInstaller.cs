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
        private Camera _camera;

        [Space, SerializeField]
        private SceneLoader _sceneLoader;

        public override void InstallBindings()
        {
            BindClasses();
            BindPrefabs();
            BindInstances();
        }

        private void BindClasses()
        {
            BindInterfacesTo<ExitGameProvider>();
            Bind<FadeExecutor>();
        }

        private void BindPrefabs()
        {
            BindFromComponentInNewPrefab(_eventSystem);
            BindFromComponentInNewPrefab(_camera);
        }

        private void BindInstances()
        {
            BindInterfacesToFromInstance(_sceneLoader);
        }
    }
}

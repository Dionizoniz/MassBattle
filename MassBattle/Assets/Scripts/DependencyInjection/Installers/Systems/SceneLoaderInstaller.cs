using MassBattle.Core.Installers;
using MassBattle.Core.SceneLoaders;
using UnityEngine;

namespace MassBattle.DependencyInjection.Installers.Systems
{
    public class SceneLoaderInstaller : ExtendedMonoInstaller
    {
        [Space, SerializeField]
        private SceneLoader _sceneLoader;

        public override void InstallBindings()
        {
            BindInterfacesToFromInstance(_sceneLoader);
        }
    }
}

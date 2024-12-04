using MassBattle.Core.Installers;
using MassBattle.Core.SceneLoaders;

namespace MassBattle.DependencyInjection.Installers.Systems
{
    public class ArtSceneLoaderInstaller : ExtendedMonoInstaller
    {
        public override void InstallBindings()
        {
            Bind<ArtSceneLoader>();
        }
    }
}

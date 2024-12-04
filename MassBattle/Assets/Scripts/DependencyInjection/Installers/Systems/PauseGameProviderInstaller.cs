using MassBattle.Core.Installers;
using MassBattle.Core.Providers;

namespace MassBattle.DependencyInjection.Installers.Systems
{
    public class PauseGameProviderInstaller : ExtendedMonoInstaller
    {
        public override void InstallBindings()
        {
            Bind<PauseGameProvider>();
        }
    }
}

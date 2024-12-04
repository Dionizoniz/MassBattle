using MassBattle.Core.Installers;
using MassBattle.Core.Providers;

namespace MassBattle.DependencyInjection.Installers.Systems
{
    public class ExitGameProviderInstaller : ExtendedMonoInstaller
    {
        public override void InstallBindings()
        {
            BindInterfacesTo<ExitGameProvider>();
        }

        public override bool IsSetupCorrect() => true;
    }
}

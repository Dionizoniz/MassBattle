using MassBattle.Core.Installers;
using MassBattle.Logic.Providers;

namespace MassBattle.DependencyInjection.Installers.Systems
{
    public class UnitsFactoryInstaller : ExtendedMonoInstaller
    {
        public override void InstallBindings()
        {
            BindInterfacesTo<UnitsFactory>();
        }
    }
}

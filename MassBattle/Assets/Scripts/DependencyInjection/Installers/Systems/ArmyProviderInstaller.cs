using MassBattle.Core.Installers;
using MassBattle.Logic.Armies;

namespace MassBattle.DependencyInjection.Installers.Systems
{
    public class ArmyProviderInstaller : ExtendedMonoInstaller
    {
        public override void InstallBindings()
        {
            BindInterfacesTo<ArmyProvider>();
        }

        public override bool IsSetupCorrect() => true;
    }
}

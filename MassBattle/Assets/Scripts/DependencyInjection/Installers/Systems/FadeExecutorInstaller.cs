using MassBattle.Core.Installers;
using MassBattle.UI.FadePanel.Executors;

namespace MassBattle.DependencyInjection.Installers.Systems
{
    public class FadeExecutorInstaller : ExtendedMonoInstaller
    {
        public override void InstallBindings()
        {
            Bind<FadeExecutor>();
        }

        public override bool IsSetupCorrect() => true;
    }
}

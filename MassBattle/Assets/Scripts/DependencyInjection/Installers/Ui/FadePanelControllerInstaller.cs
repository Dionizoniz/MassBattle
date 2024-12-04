using MassBattle.Core.Installers;
using MassBattle.UI.FadePanel;
using UnityEngine;

namespace MassBattle.DependencyInjection.Installers.Ui
{
    public class FadePanelControllerInstaller : ExtendedMonoInstaller
    {
        [SerializeField]
        private FadePanelController _fadePanelControllerToSpawn;

        public override void InstallBindings()
        {
            BindInterfacesToFromComponentInNewPrefab(_fadePanelControllerToSpawn);
        }
    }
}

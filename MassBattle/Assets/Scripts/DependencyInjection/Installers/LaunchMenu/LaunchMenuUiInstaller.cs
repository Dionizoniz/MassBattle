using MassBattle.Core.Installers;
using MassBattle.UI.FadePanel;
using MassBattle.UI.LaunchMenu;
using UnityEngine;

namespace MassBattle.DependencyInjection.Installers.LaunchMenu
{
    public class LaunchMenuUiInstaller : ExtendedMonoInstaller
    {
        [SerializeField]
        private LaunchMenuController _launchMenuController;
        [SerializeField]
        private FadePanelController _fadePanelControllerToSpawn;

        public override void InstallBindings()
        {
            BindInterfacesToFromComponentInNewPrefab(_launchMenuController);
            BindInterfacesToFromComponentInNewPrefab(_fadePanelControllerToSpawn);
        }
    }
}

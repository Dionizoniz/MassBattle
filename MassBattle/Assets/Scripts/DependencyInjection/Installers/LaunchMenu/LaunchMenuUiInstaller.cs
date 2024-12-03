using MassBattle.Core.Installers;
using MassBattle.UI.LaunchMenu;
using UnityEngine;

namespace MassBattle.DependencyInjection.Installers.LaunchMenu
{
    public class LaunchMenuUiInstaller : ExtendedMonoInstaller
    {
        [SerializeField]
        private LaunchMenuController _launchMenuController;

        public override void InstallBindings()
        {
            BindInterfacesToFromComponentInNewPrefab(_launchMenuController);
        }
    }
}

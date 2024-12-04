using MassBattle.Core.Installers;
using MassBattle.UI.LaunchMenu;
using UnityEngine;

namespace MassBattle.DependencyInjection.Installers.Ui
{
    public class LaunchMenuControllerInstaller : ExtendedMonoInstaller
    {
        [SerializeField]
        private LaunchMenuController _launchMenuController;

        public override void InstallBindings()
        {
            BindInterfacesToFromComponentInNewPrefab(_launchMenuController);
        }

        public override bool IsSetupCorrect() => _launchMenuController != null;
    }
}

using MassBattle.Core.Installers;
using MassBattle.UI.PauseMenuPanel;
using UnityEngine;

namespace MassBattle.DependencyInjection.Installers.Ui
{
    public class PauseMenuPanelControllerInstaller : ExtendedMonoInstaller
    {
        [SerializeField]
        private PauseMenuPanelController _pauseMenuPanelController;

        public override void InstallBindings()
        {
            BindInterfacesToFromComponentInNewPrefab(_pauseMenuPanelController);
        }

        public override bool IsSetupCorrect() => _pauseMenuPanelController != null;
    }
}

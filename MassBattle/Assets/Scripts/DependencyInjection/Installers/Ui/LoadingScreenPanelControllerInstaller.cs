using MassBattle.Core.Installers;
using MassBattle.UI.LoadingScenePanel;
using UnityEngine;

namespace MassBattle.DependencyInjection.Installers.Ui
{
    public class LoadingScreenPanelControllerInstaller : ExtendedMonoInstaller
    {
        [SerializeField]
        private LoadingScreenPanelController _loadingScreenPanelController;

        public override void InstallBindings()
        {
            BindInterfacesToFromComponentInNewPrefab(_loadingScreenPanelController);
        }

        public override bool IsSetupCorrect() => _loadingScreenPanelController != null;
    }
}

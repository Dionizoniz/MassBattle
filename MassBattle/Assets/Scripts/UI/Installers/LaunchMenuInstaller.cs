using MassBattle.Core.Installers;
using MassBattle.UI.FadePanel;
using MassBattle.UI.FadePanel.Executors;
using UnityEngine;

namespace MassBattle.UI.Installers
{
    public class LaunchMenuInstaller : BaseInstaller, ILaunchMenuInstaller
    {
        [SerializeField]
        private FadePanelController _fadePanelControllerToSpawn;

        private IFadePanelController _fadePanel;

        private IFadeExecutor _fadeExecutor;

        private void Awake()
        {
            SpawnPanelControllers();
            CreateInstances();
        }

        private void SpawnPanelControllers()
        {
            _fadePanel = Instantiate(_fadePanelControllerToSpawn);
        }

        private void CreateInstances()
        {
            _fadeExecutor = new FadeExecutor(_fadePanel, this);
        }

        public override bool IsSetupCorrect() => true;
    }
}

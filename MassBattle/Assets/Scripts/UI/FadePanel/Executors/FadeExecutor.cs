using MassBattle.Core.Entities.Installers;

namespace MassBattle.UI.FadePanel.Executors
{
    public class FadeExecutor : IFadeExecutor
    {
        private readonly IFadePanelController _fadePanelController;
        private readonly IInstaller _installer;

        public FadeExecutor(IFadePanelController fadePanelController, IInstaller installer)
        {
            _fadePanelController = fadePanelController;
            _installer = installer;

            _fadePanelController.FadeInInstant();
            _installer.OnSpawnFinish += _fadePanelController.FadeOut;
        }

        ~FadeExecutor()
        {
            if (_installer != null)
            {
                _installer.OnSpawnFinish -= _fadePanelController.FadeOut;
            }
        }
    }
}

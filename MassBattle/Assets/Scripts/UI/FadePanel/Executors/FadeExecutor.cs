using Zenject;

namespace MassBattle.UI.FadePanel.Executors
{
    public class FadeExecutor : IFadeExecutor
    {
        private IFadePanelController _fadePanelController;

        [Inject]
        private void Construct(IFadePanelController fadePanelController)
        {
            _fadePanelController = fadePanelController;

            _fadePanelController.FadeInInstant();
            _fadePanelController.FadeOut(); // TODO: improve functionality
        }
    }
}

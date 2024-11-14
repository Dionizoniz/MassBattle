namespace MassBattle.UI.FadePanel.Executors
{
    public abstract class FadeExecutor : IFadeExecutor
    {
        protected IFadePanelController _fadePanelController;

        public FadeExecutor(IFadePanelController fadePanelController)
        {
            _fadePanelController = fadePanelController;
        }
    }
}

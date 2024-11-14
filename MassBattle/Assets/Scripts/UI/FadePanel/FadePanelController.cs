using MassBattle.Core.Entities.MVC;

namespace MassBattle.UI.FadePanel
{
    public class FadePanelController : Controller<FadePanelModel, FadePanelView>, IFadePanelController
    {
        public void FadeIn(bool isInstant = false)
        {
            _view.FadeIn(isInstant);
        }

        public void FadeOut(bool isInstant = false)
        {
            _view.FadeOut(isInstant);
        }
    }
}

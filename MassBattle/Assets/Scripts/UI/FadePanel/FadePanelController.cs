using MassBattle.Core.Patterns.MVC;

namespace MassBattle.UI.FadePanel
{
    public class FadePanelController : Controller<FadePanelModel, FadePanelView>, IFadePanelController
    {
        public void FadeIn()
        {
            _view.FadeIn(false);
        }

        public void FadeInInstant()
        {
            _view.FadeIn(true);
        }

        public void FadeOut()
        {
            _view.FadeOut(false);
        }

        public void FadeOutInstant()
        {
            _view.FadeOut(true);
        }
    }
}

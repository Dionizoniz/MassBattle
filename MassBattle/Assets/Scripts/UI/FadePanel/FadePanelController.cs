using MassBattle.Core.Entities.MVC;

namespace MassBattle.UI.FadePanel
{
    public class FadePanelController : Controller<FadePanelModel, FadePanelView>, IFadePanelController
    {
        public void FadeIn()
        {
            _view.FadeIn();
        }

        public void FadeOut()
        {
            _view.FadeOut();
        }
    }
}

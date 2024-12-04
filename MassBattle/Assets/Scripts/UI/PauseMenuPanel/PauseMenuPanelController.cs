using MassBattle.Core.Patterns.MVC;

namespace MassBattle.UI.PauseMenuPanel
{
    public class PauseMenuPanelController : Controller<PauseMenuPanelModel, PauseMenuPanelView>,
                                            IPauseMenuPanelController
    {
        public void OpenPauseMenuPanel()
        {
            _model.OpenPauseMenuPanel();
        }

        public void Continue()
        {
            _model.Continue();
        }

        public void ExitBattle()
        {
            _model.ExitBattle();
        }
    }
}

using MassBattle.Core.Entities.MVC;
using MassBattle.Core.SceneLoaders;
using MassBattle.Core.UserInput;

namespace MassBattle.UI.PauseMenuPanel
{
    public class PauseMenuPanelModel : Model<PauseMenuPanelView>
    {
        private IInputFacade _inputFacade;
        private ISceneLoader _sceneLoader;

        public void InjectData(IInputFacade inputFacade, ISceneLoader sceneLoader)
        {
            _inputFacade = inputFacade;
            _sceneLoader = sceneLoader;
        }

        public void OpenPauseMenuPanel()
        {
            // TODO pause
            _view.ShowContentPanel();
        }

        public void Continue()
        {
            // TODO unpause
            _view.HideContentPanel();
        }

        public void ExitBattle()
        {
            // TODO unpause
        }
    }
}

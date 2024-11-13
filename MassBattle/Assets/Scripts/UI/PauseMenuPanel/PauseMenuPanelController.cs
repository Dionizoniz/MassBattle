using MassBattle.Core.Entities.MVC;
using MassBattle.Core.SceneLoaders;
using MassBattle.Core.UserInput;

namespace MassBattle.UI.PauseMenuPanel
{
    public class PauseMenuPanelController : Controller<PauseMenuPanelModel, PauseMenuPanelView>,
                                            IPauseMenuPanelController
    {
        private IInputFacade _inputFacade;
        private ISceneLoader _sceneLoader;

        public void InjectData(IInputFacade inputFacade, ISceneLoader sceneLoader)
        {
            _inputFacade = inputFacade;
            _sceneLoader = sceneLoader;
        }

        protected override void Initialize()
        {
            base.Initialize();

            _view.Initialize();
            _model.InjectData(_inputFacade, _sceneLoader);
        }

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

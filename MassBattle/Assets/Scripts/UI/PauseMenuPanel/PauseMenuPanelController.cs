using MassBattle.Core.Entities.MVC;
using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Core.UserInput;

namespace MassBattle.UI.PauseMenuPanel
{
    public class PauseMenuPanelController : Controller<PauseMenuPanelModel, PauseMenuPanelView>,
                                            IPauseMenuPanelController
    {
        private IInputFacade _inputFacade;
        private ISceneLoader _sceneLoader;
        private IPauseGameProvider _pauseGameProvider;

        public void InjectData(IInputFacade inputFacade, ISceneLoader sceneLoader, IPauseGameProvider pauseGameProvider)
        {
            _inputFacade = inputFacade;
            _sceneLoader = sceneLoader;
            _pauseGameProvider = pauseGameProvider;
        }

        protected override void Initialize()
        {
            base.Initialize();

            _view.Initialize();
            _model.InjectData(_inputFacade, _sceneLoader, _pauseGameProvider);
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

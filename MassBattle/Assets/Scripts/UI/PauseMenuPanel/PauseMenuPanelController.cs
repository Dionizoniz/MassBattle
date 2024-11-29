using MassBattle.Core.Patterns.MVC;
using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Core.UserInput;
using MassBattle.Logic.Armies;

namespace MassBattle.UI.PauseMenuPanel
{
    public class PauseMenuPanelController : Controller<PauseMenuPanelModel, PauseMenuPanelView>,
                                            IPauseMenuPanelController
    {
        private IInputFacade _inputFacade;
        private ISceneLoader _sceneLoader;
        private IPauseGameProvider _pauseGameProvider;
        private IArmyProvider _armyProvider;

        public void InjectData(
                IInputFacade inputFacade, ISceneLoader sceneLoader, IPauseGameProvider pauseGameProvider,
                IArmyProvider armyProvider)
        {
            _inputFacade = inputFacade;
            _sceneLoader = sceneLoader;
            _pauseGameProvider = pauseGameProvider;
            _armyProvider = armyProvider;
        }

        protected override void Initialize()
        {
            base.Initialize();

            _view.Initialize(_armyProvider);
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

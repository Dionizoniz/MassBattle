using MassBattle.Core.Entities.MVC;
using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Core.UserInput;

namespace MassBattle.UI.PauseMenuPanel
{
    public class PauseMenuPanelModel : Model<PauseMenuPanelView>
    {
        private IInputFacade _inputFacade;
        private ISceneLoader _sceneLoader;
        private IPauseGameProvider _pauseGameProvider;

        public void InjectData(IInputFacade inputFacade, ISceneLoader sceneLoader, IPauseGameProvider pauseGameProvider)
        {
            _inputFacade = inputFacade;
            _sceneLoader = sceneLoader;
            _pauseGameProvider = pauseGameProvider;

            _inputFacade.OnExitButton += TogglePauseMenuPanel;
        }

        private void TogglePauseMenuPanel()
        {
            if (_view.IsPanelVisible)
            {
                Continue();
            }
            else
            {
                OpenPauseMenuPanel();
            }
        }

        public void Continue()
        {
            _pauseGameProvider.ResumeGame();
            _view.HideContentPanel();
        }

        public void OpenPauseMenuPanel()
        {
            _pauseGameProvider.PauseGame();
            _view.ShowContentPanel();
        }

        public void ExitBattle()
        {
            _pauseGameProvider.ResumeGame();
            _sceneLoader.LoadLaunchMenuScene();
        }

        private void OnDestroy()
        {
            if (_inputFacade != null)
            {
                _inputFacade.OnExitButton -= TogglePauseMenuPanel;
            }
        }
    }
}

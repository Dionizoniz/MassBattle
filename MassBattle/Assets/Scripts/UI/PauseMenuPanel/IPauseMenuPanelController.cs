using MassBattle.Core.SceneLoaders;
using MassBattle.Core.UserInput;

namespace MassBattle.UI.PauseMenuPanel
{
    public interface IPauseMenuPanelController
    {
        void InjectData(IInputFacade inputFacade, ISceneLoader sceneLoader);
    }
}

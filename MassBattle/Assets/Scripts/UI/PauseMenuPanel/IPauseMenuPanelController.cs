using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Core.UserInput;
using MassBattle.Logic.Armies;

namespace MassBattle.UI.PauseMenuPanel
{
    public interface IPauseMenuPanelController
    {
        void InjectData(
                IInputFacade inputFacade, ISceneLoader sceneLoader, IPauseGameProvider pauseGameProvider,
                IArmyProvider armyProvider);
    }
}

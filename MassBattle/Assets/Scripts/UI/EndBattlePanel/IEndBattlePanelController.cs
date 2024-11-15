using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.Armies;

namespace MassBattle.UI.EndBattlePanel
{
    public interface IEndBattlePanelController
    {
        void InjectData(IArmyProvider armyProvider, ISceneLoader sceneLoader, IPauseGameProvider pauseGameProvider);
    }
}

using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.Armies;

namespace MassBattle.UI.EndBattlePanel
{
    public interface IEndBattlePanelController
    {
        void InjectData(IArmyProvider armyProvider, SceneLoader sceneLoader);
    }
}

using MassBattle.Core.Patterns.MVC;
using MassBattle.Core.Providers;
using MassBattle.Logic.Armies;
using Zenject;

namespace MassBattle.UI.EndBattlePanel
{
    public class EndBattlePanelModel : Model<EndBattlePanelView>
    {
        private IArmyProvider _armyProvider;
        private IPauseGameProvider _pauseGameProvider;

        [Inject]
        public void Construct(IArmyProvider armyProvider, IPauseGameProvider pauseGameProvider)
        {
            _armyProvider = armyProvider;
            _pauseGameProvider = pauseGameProvider;

            _armyProvider.OnLastArmyStay += ShowContentPanelForArmy;
            _armyProvider.OnNoArmyStay += ShowContentPanelForDraw;
        }

        private void ShowContentPanelForArmy(ArmyData armyData)
        {
            _pauseGameProvider.PauseGame();
            _view.ShowContentPanelFor(armyData);
        }

        private void ShowContentPanelForDraw()
        {
            _pauseGameProvider.PauseGame();
            _view.ShowContentPanelForDraw();
        }

        public void ResumeGame()
        {
            _pauseGameProvider.ResumeGame();
        }

        private void OnDestroy()
        {
            if (_armyProvider != null)
            {
                _armyProvider.OnLastArmyStay -= ShowContentPanelForArmy;
            }
        }
    }
}

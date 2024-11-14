using MassBattle.Core.Entities.MVC;
using MassBattle.Logic.Armies;

namespace MassBattle.UI.EndBattlePanel
{
    public class EndBattlePanelModel : Model<EndBattlePanelView>
    {
        private IArmyProvider _armyProvider;

        public void InjectData(IArmyProvider armyProvider)
        {
            _armyProvider = armyProvider;
            _armyProvider.OnLastArmyStay += TryShowContentPanelForArmy;
            _armyProvider.OnNoArmyStay += TryShowContentPanelForDraw;
        }

        private void TryShowContentPanelForArmy(ArmyData armyData)
        {
            _view.ShowContentPanelFor(armyData);
        }

        private void TryShowContentPanelForDraw()
        {
            _view.ShowContentPanelForDraw();
        }

        private void OnDestroy()
        {
            if (_armyProvider != null)
            {
                _armyProvider.OnLastArmyStay -= TryShowContentPanelForArmy;
            }
        }
    }
}

using System;
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
            _armyProvider.OnLastArmyStay += TryShowContentPanel;
        }

        private void TryShowContentPanel(ArmyData armyData)
        {
            _view.ShowContentPanel(armyData);
        }

        private void OnDestroy()
        {
            if (_armyProvider != null)
            {
                _armyProvider.OnLastArmyStay -= TryShowContentPanel;
            }
        }
    }
}

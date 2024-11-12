using MassBattle.Core.Entities.MVC;
using MassBattle.Logic.Armies;
using UnityEngine;

namespace MassBattle.UI.EndBattlePanel
{
    public class EndBattlePanelView : View
    {
        [SerializeField]
        private GameObject _contentPanel;

        public void Initialize()
        {
            HideContentPanel();
        }

        private void HideContentPanel()
        {
            _contentPanel.SetActive(false);
        }

        public void ShowContentPanel(ArmyData armyData)
        {
            _contentPanel.SetActive(true);
        }
    }
}

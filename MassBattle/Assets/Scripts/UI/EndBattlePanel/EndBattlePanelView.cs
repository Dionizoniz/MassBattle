using MassBattle.Core.Entities.MVC;
using MassBattle.Logic.Armies;
using TMPro;
using UnityEngine;

namespace MassBattle.UI.EndBattlePanel
{
    public class EndBattlePanelView : View
    {
        [SerializeField]
        private GameObject _contentPanel;
        [SerializeField]
        private TextMeshProUGUI _winnerArmyNameLabel;

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
            _winnerArmyNameLabel.text = armyData.ArmySetup.ArmyId;
            _contentPanel.SetActive(true);
        }
    }
}

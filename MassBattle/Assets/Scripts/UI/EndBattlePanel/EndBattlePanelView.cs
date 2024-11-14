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

        [Space, SerializeField]
        private GameObject _winnerArmyRoot;
        [SerializeField]
        private TextMeshProUGUI _winnerArmyNameLabel;

        [Space, SerializeField]
        private GameObject _drawBattleRoot;

        public void Initialize()
        {
            HideContentPanel();
        }

        private void HideContentPanel()
        {
            _contentPanel.SetActive(false);
        }

        public void ShowContentPanelFor(ArmyData armyData)
        {
            _winnerArmyRoot.SetActive(true);
            _drawBattleRoot.SetActive(false);

            _winnerArmyNameLabel.text = armyData.ArmySetup.ArmyName;
            ShowContentPanel();
        }

        private void ShowContentPanel()
        {
            _contentPanel.SetActive(true);
        }

        public void ShowContentPanelForDraw()
        {
            _winnerArmyRoot.SetActive(false);
            _drawBattleRoot.SetActive(true);

            ShowContentPanel();
        }
    }
}

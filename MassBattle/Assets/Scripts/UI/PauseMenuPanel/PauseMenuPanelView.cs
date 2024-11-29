using MassBattle.Core.Patterns.MVC;
using MassBattle.Logic.Armies;
using UnityEngine;

namespace MassBattle.UI.PauseMenuPanel
{
    public class PauseMenuPanelView : View
    {
        [SerializeField]
        private GameObject _contentPanel;
        [SerializeField]
        private GameObject _openPauseMenuButton;
        private IArmyProvider _armyProvider;

        public bool IsPanelVisible { get; private set; }

        public void Initialize(IArmyProvider armyProvider)
        {
            _armyProvider = armyProvider;

            _armyProvider.OnLastArmyStay += HideAllElements;
            _armyProvider.OnNoArmyStay += HideAllElements;

            HideContentPanel();
        }

        private void HideAllElements(ArmyData _)
        {
            HideAllElements();
        }

        private void HideAllElements()
        {
            _openPauseMenuButton.SetActive(false);
            _contentPanel.SetActive(false);

            IsPanelVisible = false;
        }

        public void HideContentPanel()
        {
            _openPauseMenuButton.SetActive(true);
            _contentPanel.SetActive(false);

            IsPanelVisible = false;
        }

        public void ShowContentPanel()
        {
            _openPauseMenuButton.SetActive(false);
            _contentPanel.SetActive(true);

            IsPanelVisible = true;
        }

        private void OnDestroy()
        {
            if (_armyProvider != null)
            {
                _armyProvider.OnLastArmyStay -= HideAllElements;
                _armyProvider.OnNoArmyStay -= HideAllElements;
            }
        }
    }
}

using MassBattle.Core.Entities.MVC;
using UnityEngine;

namespace MassBattle.UI.PauseMenuPanel
{
    public class PauseMenuPanelView : View
    {
        [SerializeField]
        private GameObject _contentPanel;
        [SerializeField]
        private GameObject _openPauseMenuButton;

        public void Initialize()
        {
            HideContentPanel();
        }

        public void ShowContentPanel()
        {
            _openPauseMenuButton.SetActive(false);
            _contentPanel.SetActive(true);
        }

        public void HideContentPanel()
        {
            _openPauseMenuButton.SetActive(true);
            _contentPanel.SetActive(false);
        }
    }
}

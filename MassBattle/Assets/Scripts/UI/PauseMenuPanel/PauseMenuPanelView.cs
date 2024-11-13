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

        private bool _isPanelVisible;

        public void Initialize()
        {
            HideContentPanel();
        }

        public void ToggleContentPanel()
        {
            if (_isPanelVisible)
            {
                HideContentPanel();
            }
            else
            {
                ShowContentPanel();
            }
        }

        public void HideContentPanel()
        {
            _openPauseMenuButton.SetActive(true);
            _contentPanel.SetActive(false);

            _isPanelVisible = false;
        }

        public void ShowContentPanel()
        {
            _openPauseMenuButton.SetActive(false);
            _contentPanel.SetActive(true);

            _isPanelVisible = true;
        }
    }
}

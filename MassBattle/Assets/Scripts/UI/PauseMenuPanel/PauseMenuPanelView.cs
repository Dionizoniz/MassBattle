using MassBattle.Core.Entities.MVC;
using UnityEngine;

namespace MassBattle.UI.PauseMenuPanel
{
    public class PauseMenuPanelView : View
    {
        [SerializeField]
        private GameObject _contentPanel;

        public void Initialize()
        {
            HideContentPanel();
        }

        public void ShowContentPanel()
        {
            _contentPanel.SetActive(true);
        }

        private void HideContentPanel()
        {
            _contentPanel.SetActive(false);
        }
    }
}

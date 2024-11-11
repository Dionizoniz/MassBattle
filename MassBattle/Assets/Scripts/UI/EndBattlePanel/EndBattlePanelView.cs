using MassBattle.Core.Entities.MVC;
using UnityEngine;

namespace MassBattle.UI.EndBattlePanel
{
    public class EndBattlePanelView : View
    {
        [SerializeField]
        private GameObject _contentPanel;

        public void Initialize()
        {
            _contentPanel.SetActive(false);
        }
    }
}

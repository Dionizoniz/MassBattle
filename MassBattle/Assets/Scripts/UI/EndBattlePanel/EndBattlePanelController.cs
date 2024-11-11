using MassBattle.Core.Entities.MVC;
using UnityEngine.SceneManagement;

namespace MassBattle.UI.EndBattlePanel
{
    public class EndBattlePanelController : Controller<EndBattlePanelModel, EndBattlePanelView>
    {
        public void GoToMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}

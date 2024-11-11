using MassBattle.Core.Entities.MVC;
using UnityEngine.SceneManagement;

namespace MassBattle.UI.GameOverMenu
{
    public class GameOverPanelController : Controller<GameOverPanelModel, GameOverPanelView>
    {
        public void GoToMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}

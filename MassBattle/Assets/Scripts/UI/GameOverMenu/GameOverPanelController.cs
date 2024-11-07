using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MassBattle.UI.GameOverMenu
{
    public class GameOverPanelController : MonoBehaviour // TODO refactor and restore functionality
    {
        public TextMeshProUGUI armyWins;
        public Button goToMenu;

        public void Populate() // TODO events reaction
        {
            // if ( BattleInstantiator.instance.army1.GetUnits().Count == 0 )
            // {
            //     armyWins.text = "Army 1 wins!";
            // }
            //
            // if ( BattleInstantiator.instance.army2.GetUnits().Count == 0 )
            // {
            //     armyWins.text = "Army 2 wins!";
            // }
        }

        private void Awake()
        {
            goToMenu.onClick.AddListener(GoToMenu);
        }

        private void GoToMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}

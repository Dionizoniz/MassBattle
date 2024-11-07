using MassBattle.Core.Patterns;
using MassBattle.Logic.Setup;
using UnityEngine.SceneManagement;

namespace MassBattle.UI.LaunchMenu
{
    public class LaunchMenuModel : Model<LaunchMenuView>
    {
        public void StartBattle(BattleSetup battleSetup)
        {
            ClearRegisteredArmySetups(battleSetup);
            RegisterArmiesSetup(battleSetup);
            LoadBattleScene();
        }

        private void ClearRegisteredArmySetups(BattleSetup battleSetup)
        {
            battleSetup.ClearSavedArmySetups();
        }

        private void RegisterArmiesSetup(BattleSetup battleSetup)
        {
            foreach (var panel in _view.ArmyPanels)
            {
                ArmySetup armySetup = panel.CreateArmySetup();
                battleSetup.SaveArmySetup(armySetup);
            }
        }

        private void LoadBattleScene() // TODO improve
        {
            SceneManager.LoadScene(1);
        }
    }
}

using MassBattle.Core.Entities.MVC;
using MassBattle.Logic.Setup;
using UnityEngine.SceneManagement;

namespace MassBattle.UI.LaunchMenu
{
    public class LaunchMenuModel : Model<LaunchMenuView>
    {
        public void StartBattle(IBattleSetup battleSetup)
        {
            ClearRegisteredArmySetups(battleSetup);
            RegisterArmiesSetup(battleSetup);
            LoadBattleScene();
        }

        private void ClearRegisteredArmySetups(IBattleSetup battleSetup)
        {
            battleSetup.ClearSavedArmySetups();
        }

        private void RegisterArmiesSetup(IBattleSetup battleSetup)
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

using MassBattle.Core.Entities.MVC;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.Armies;
using MassBattle.Logic.BattleCreator;

namespace MassBattle.UI.LaunchMenu
{
    public class LaunchMenuModel : Model<LaunchMenuView>
    {
        private ISceneLoader _sceneLoader;

        public void InjectData(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void StartBattle(IBattleSetup battleSetup)
        {
            // TODO add test setup logic to avoid the same Army Names. 

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

        private void LoadBattleScene()
        {
            _sceneLoader.LoadBattleScene();
        }
    }
}

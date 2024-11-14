using System.Collections.Generic;
using System.Linq;
using MassBattle.Core.Entities.MVC;
using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.Armies;
using MassBattle.Logic.BattleCreator;

namespace MassBattle.UI.LaunchMenu
{
    public class LaunchMenuModel : Model<LaunchMenuView>
    {
        private ISceneLoader _sceneLoader;
        private IExitGameProvider _exitGameProvider;

        public void InjectData(ISceneLoader sceneLoader, IExitGameProvider exitGameProvider)
        {
            _sceneLoader = sceneLoader;
            _exitGameProvider = exitGameProvider;
        }

        public void StartBattle(IBattleSetup battleSetup)
        {
            if (IsCorrectArmyIdsSetup())
            {
                ClearRegisteredArmySetups(battleSetup);
                RegisterArmiesSetup(battleSetup);
                LoadBattleScene();
            }
            else
            {
                _view.ShowArmyIdsErrorMessage();
            }
        }

        private bool IsCorrectArmyIdsSetup()
        {
            IEnumerable<string> activeIds = _view.ArmyPanels.Select(panel => panel.ArmyId);

            List<string> duplicateIds = activeIds.GroupBy(id => id)
                                                 .Where(group => group.Count() > 1)
                                                 .Select(group => group.Key)
                                                 .ToList();

            return duplicateIds.Count == 0;
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

        public void ExitGame()
        {
            _exitGameProvider.ExitGame();
        }
    }
}

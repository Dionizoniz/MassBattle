using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MassBattle.Core.Entities.MVC;
using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.Armies;
using MassBattle.Logic.BattleCreator;
using UnityEngine;

namespace MassBattle.UI.LaunchMenu
{
    public class LaunchMenuModel : Model<LaunchMenuView>
    {
        [SerializeField]
        private float _applicationExitDelay = 3f;

        private ISceneLoader _sceneLoader;
        private IExitGameProvider _exitGameProvider;

        public void InjectData(ISceneLoader sceneLoader, IExitGameProvider exitGameProvider)
        {
            _sceneLoader = sceneLoader;
            _exitGameProvider = exitGameProvider;
        }

        public void StartBattle(IBattleSetup battleSetup)
        {
            if (IsCorrectArmyNamesSetup())
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

        private bool IsCorrectArmyNamesSetup()
        {
            // TODO add active armies check only !!!
            IEnumerable<string> activeIds = _view.ArmyPanels.Select(panel => panel.ArmyName);

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
            for (var i = 0; i < _view.ArmyPanels.Count; i++)
            {
                var panel = _view.ArmyPanels[i];
                ArmySetup armySetup = panel.CreateArmySetup(i);
                battleSetup.SaveArmySetup(armySetup);
            }
        }

        private void LoadBattleScene()
        {
            _sceneLoader.LoadBattleScene();
        }

        public void ExitGame()
        {
            StartExitProcess();
        }

        private void StartExitProcess()
        {
            StartCoroutine(ExitProcess());
        }

        private IEnumerator ExitProcess()
        {
            _view.ShowExitPanel();

            yield return new WaitForSeconds(_applicationExitDelay);

            _exitGameProvider.ExitGame();
        }
    }
}

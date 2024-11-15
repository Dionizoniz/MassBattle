using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MassBattle.Core.Entities.MVC;
using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.Databases.ArmyDatabase;
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

        public void StartBattle(IArmyDatabase armyDatabase)
        {
            if (IsCorrectArmyNamesSetup())
            {
                ClearRegisteredArmySetups(armyDatabase);
                RegisterArmiesSetup(armyDatabase);
                LoadBattleScene();
            }
            else
            {
                _view.ShowArmyNamesErrorMessage();
            }
        }

        private bool IsCorrectArmyNamesSetup()
        {
            IEnumerable<string> activeIds = FindActiveArmiesNames();

            List<string> duplicateIds = activeIds.GroupBy(id => id)
                                                 .Where(group => group.Count() > 1)
                                                 .Select(group => group.Key)
                                                 .ToList();

            return duplicateIds.Count == 0;
        }

        private IEnumerable<string> FindActiveArmiesNames()
        {
            foreach (ArmyPanelController armyPanel in _view.ArmyPanels)
            {
                if (armyPanel.IsArmyActive)
                {
                    yield return armyPanel.ArmyName;
                }
            }
        }

        private void ClearRegisteredArmySetups(IArmyDatabase armyDatabase)
        {
            armyDatabase.ClearSavedArmiesData();
        }

        private void RegisterArmiesSetup(IArmyDatabase armyDatabase)
        {
            foreach (var panel in _view.ArmyPanels)
            {
                InitialArmyData armySetup = panel.CreateArmySetup();
                armyDatabase.SaveArmyData(armySetup);
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

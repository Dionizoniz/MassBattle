using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MassBattle.Core.Patterns.MVC;
using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.Databases.Armies;
using UnityEngine;
using Zenject;

namespace MassBattle.UI.LaunchMenu
{
    public class LaunchMenuModel : Model<LaunchMenuView>
    {
        [SerializeField]
        private float _applicationExitDelay = 3f;

        private ISceneLoader _sceneLoader;
        private IBattleSetup _battleSetup;
        private IExitGameProvider _exitGameProvider;

        [Inject]
        private void Construct(ISceneLoader sceneLoader, IBattleSetup battleSetup, IExitGameProvider exitGameProvider)
        {
            _sceneLoader = sceneLoader;
            _battleSetup = battleSetup;
            _exitGameProvider = exitGameProvider;
        }

        public void StartBattle()
        {
            bool isCorrectArmyNamesSetup = IsCorrectArmyNamesSetup();
            bool isCorrectArmyColorsSetup = IsCorrectArmyColorsSetup();

            if (isCorrectArmyNamesSetup && isCorrectArmyColorsSetup)
            {
                ClearRegisteredArmySetups();
                RegisterArmiesSetup();
                LoadBattleScene();
            }
            else if (isCorrectArmyNamesSetup == false)
            {
                _view.ShowArmyNamesErrorMessage();
            }
            else
            {
                _view.ShowArmyColorsErrorMessage();
            }
        }

        private bool IsCorrectArmyNamesSetup()
        {
            List<string> names = FindActiveArmiesNames();
            return IsNoDuplicates(names);
        }

        private List<string> FindActiveArmiesNames()
        {
            return _view.ArmyPanels.Where(panel => panel.IsArmyActive).Select(panel => panel.ArmyName).ToList();
        }

        private bool IsNoDuplicates<T>(List<T> items)
        {
            List<T> duplicateItems = items.GroupBy(id => id)
                                          .Where(group => group.Count() > 1)
                                          .Select(group => group.Key)
                                          .ToList();

            return duplicateItems.Count == 0;
        }

        private bool IsCorrectArmyColorsSetup()
        {
            List<Color> colors = FindActiveArmiesColors();
            return IsNoDuplicates(colors);
        }

        private List<Color> FindActiveArmiesColors()
        {
            return _view.ArmyPanels.Where(panel => panel.IsArmyActive).Select(panel => panel.ArmyColor).ToList();
        }

        private void ClearRegisteredArmySetups()
        {
            _battleSetup.ClearSavedArmiesData();
        }

        private void RegisterArmiesSetup()
        {
            foreach (var panel in _view.ArmyPanels)
            {
                InitialArmyData armySetup = panel.CreateArmySetup();
                _battleSetup.SaveArmyData(armySetup);
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

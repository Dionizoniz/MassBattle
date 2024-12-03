using MassBattle.Core.Patterns.MVC;
using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.Databases.Armies;
using MassBattle.Logic.Databases.Colors;
using MassBattle.Logic.Databases.Units;
using UnityEngine;
using Zenject;

namespace MassBattle.UI.LaunchMenu
{
    public class LaunchMenuController : Controller<LaunchMenuModel, LaunchMenuView>, ILaunchMenuController
    {
        [Space, SerializeField]
        private ArmyDatabase _armyDatabase;
        [SerializeField]
        private ColorDatabase _colorDatabase;
        [SerializeField]
        private UnitDatabase _unitDatabase;
        [SerializeField]
        private SceneLoader _sceneLoader;

        // private IArmyDatabase _armyDatabase;
        // private IColorDatabase _colorDatabase;
        // private ISceneLoader _sceneLoader;
        // private IUnitDatabase _unitDatabase;

        public void InjectData(
                IArmyDatabase battleSetup, IColorDatabase colorDatabase, ISceneLoader sceneLoader,
                IUnitDatabase unitDatabase)
        {
            // _armyDatabase = battleSetup;
            // _colorDatabase = colorDatabase;
            // _sceneLoader = sceneLoader;
            // _unitDatabase = unitDatabase;
        }

        public void StartBattle()
        {
            _model.StartBattle(_armyDatabase);
        }

        public void ExitGame()
        {
            _model.ExitGame();
        }

        protected override void Initialize()
        {
            base.Initialize();
            _view.SpawnPanels(_armyDatabase, _colorDatabase, _unitDatabase);
            _model.InjectData(_sceneLoader);
        }
    }
}

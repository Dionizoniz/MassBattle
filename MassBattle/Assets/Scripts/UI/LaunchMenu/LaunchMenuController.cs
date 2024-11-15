using MassBattle.Core.Entities.MVC;
using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.BattleCreator;
using MassBattle.Logic.Databases;
using MassBattle.Logic.Databases.ArmyDatabase;
using MassBattle.Logic.Databases.Colors;

namespace MassBattle.UI.LaunchMenu
{
    public class LaunchMenuController : Controller<LaunchMenuModel, LaunchMenuView>, ILaunchMenuController
    {
        private IArmyDatabase _armyDatabase;
        private IColorDatabase _colorDatabase;
        private ISceneLoader _sceneLoader;
        private IExitGameProvider _exitGameProvider;

        public void InjectData(
                IArmyDatabase battleSetup, IColorDatabase colorDatabase, ISceneLoader sceneLoader,
                IExitGameProvider exitGameProvider)
        {
            _armyDatabase = battleSetup;
            _colorDatabase = colorDatabase;
            _sceneLoader = sceneLoader;
            _exitGameProvider = exitGameProvider;
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
            _view.SpawnPanels(_armyDatabase, _colorDatabase);
            _model.InjectData(_sceneLoader, _exitGameProvider);
        }
    }
}

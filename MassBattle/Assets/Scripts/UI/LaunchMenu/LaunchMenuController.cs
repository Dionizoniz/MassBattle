using MassBattle.Core.Entities.MVC;
using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.BattleCreator;
using MassBattle.Logic.Databases;
using MassBattle.Logic.Databases.Colors;

namespace MassBattle.UI.LaunchMenu
{
    public class LaunchMenuController : Controller<LaunchMenuModel, LaunchMenuView>, ILaunchMenuController
    {
        private IBattleSetup _battleSetup;
        private IColorDatabase _colorDatabase;
        private ISceneLoader _sceneLoader;
        private IExitGameProvider _exitGameProvider;

        public void InjectData(
                IBattleSetup battleSetup, IColorDatabase colorDatabase, ISceneLoader sceneLoader,
                IExitGameProvider exitGameProvider)
        {
            _battleSetup = battleSetup;
            _colorDatabase = colorDatabase;
            _sceneLoader = sceneLoader;
            _exitGameProvider = exitGameProvider;
        }

        public void StartBattle()
        {
            _model.StartBattle(_battleSetup);
        }

        public void ExitGame()
        {
            _model.ExitGame();
        }

        protected override void Initialize()
        {
            base.Initialize();
            _view.SpawnPanels(_battleSetup, _colorDatabase);
            _model.InjectData(_sceneLoader, _exitGameProvider);
        }
    }
}

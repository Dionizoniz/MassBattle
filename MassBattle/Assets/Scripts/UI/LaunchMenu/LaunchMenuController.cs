using MassBattle.Core.Entities.MVC;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.BattleCreator;
using MassBattle.Logic.Databases;

namespace MassBattle.UI.LaunchMenu
{
    public class LaunchMenuController : Controller<LaunchMenuModel, LaunchMenuView>, ILaunchMenuController
    {
        private IBattleSetup _battleSetup;
        private ColorDatabase _colorDatabase;
        private SceneLoader _sceneLoader;

        public void InjectData(IBattleSetup battleSetup, ColorDatabase colorDatabase, SceneLoader sceneLoader)
        {
            _battleSetup = battleSetup;
            _colorDatabase = colorDatabase;
            _sceneLoader = sceneLoader;
        }

        public void StartBattle()
        {
            _model.StartBattle(_battleSetup);
        }

        protected override void Initialize()
        {
            base.Initialize();
            _view.SpawnPanels(_battleSetup, _colorDatabase);
            _model.InjectData(_sceneLoader);
        }
    }
}

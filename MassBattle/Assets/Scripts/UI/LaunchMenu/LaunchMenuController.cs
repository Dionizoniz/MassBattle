using MassBattle.Core.Entities.MVC;
using MassBattle.Logic.Databases;
using MassBattle.Logic.Setup;

namespace MassBattle.UI.LaunchMenu
{
    public class LaunchMenuController : Controller<LaunchMenuModel, LaunchMenuView>, ILaunchMenuController
    {
        private IBattleSetup _battleSetup;
        private ColorDatabase _colorDatabase;

        public void InjectData(IBattleSetup battleSetup, ColorDatabase colorDatabase)
        {
            _battleSetup = battleSetup;
            _colorDatabase = colorDatabase;
        }

        public void StartBattle()
        {
            _model.StartBattle(_battleSetup);
        }

        protected override void Initialize()
        {
            base.Initialize();

            _view.SpawnPanels(_battleSetup, _colorDatabase);
        }
    }
}

using MassBattle.Core.Entities.MVC;
using MassBattle.Logic.Databases;
using MassBattle.Logic.Setup;
using UnityEngine;

namespace MassBattle.UI.LaunchMenu
{
    public class LaunchMenuController : Controller<LaunchMenuModel, LaunchMenuView>, ILaunchMenuController
    {
        [SerializeField]
        private BattleSetup _battleSetup;

        private ColorDatabase _colorDatabase;

        public void InjectData(BattleSetup battleSetup, ColorDatabase colorDatabase)
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

            _view.InitializePanels(_battleSetup, _colorDatabase);
        }
    }
}

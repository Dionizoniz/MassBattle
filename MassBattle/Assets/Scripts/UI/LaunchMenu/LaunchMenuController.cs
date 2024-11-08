using MassBattle.Core.Patterns;
using MassBattle.Logic.Setup;
using UnityEngine;

namespace MassBattle.UI.LaunchMenu
{
    public class LaunchMenuController : Controller<LaunchMenuModel, LaunchMenuView>
    {
        [SerializeField]
        private BattleSetup _battleSetup;

        public void InjectData(BattleSetup battleSetup)
        {
            _battleSetup = battleSetup;
        }

        public void StartBattle()
        {
            _model.StartBattle(_battleSetup);
        }

        protected override void Initialize()
        {
            base.Initialize();

            _view.InitializePanels(_battleSetup);
        }
    }
}

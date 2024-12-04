using System;
using MassBattle.Core.Entities;
using MassBattle.Core.Patterns.MVC;

namespace MassBattle.UI.LaunchMenu
{
    public class LaunchMenuController : Controller<LaunchMenuModel, LaunchMenuView>, ILaunchMenuController,
                                        ISceneSpawner
    {
        public event Action OnSpawnScene = delegate { };

        public bool IsSceneSpawned { get; private set; }

        protected override void Initialize()
        {
            base.Initialize();
            _view.SpawnPanels();

            IsSceneSpawned = true;
            OnSpawnScene.Invoke();
        }

        public void StartBattle()
        {
            _model.StartBattle();
        }

        public void ExitGame()
        {
            _model.ExitGame();
        }

        private void OnDestroy()
        {
            IsSceneSpawned = false;
        }
    }
}

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
        public void StartBattle()
        {
            _model.StartBattle();
        }

        public void ExitGame()
        {
            _model.ExitGame();
        }
    }
}

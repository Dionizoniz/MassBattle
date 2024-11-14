using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.BattleCreator;
using MassBattle.Logic.Databases;

namespace MassBattle.UI.LaunchMenu
{
    public interface ILaunchMenuController
    {
        void InjectData(
                IBattleSetup battleSetup, IColorDatabase colorDatabase, ISceneLoader sceneLoader,
                IExitGameProvider exitGameProvider);
    }
}

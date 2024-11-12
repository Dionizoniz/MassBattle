using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.BattleCreator;
using MassBattle.Logic.Databases;

namespace MassBattle.UI.LaunchMenu
{
    public interface ILaunchMenuController
    {
        void InjectData(IBattleSetup battleSetup, ColorDatabase colorDatabase, SceneLoader sceneLoader);
    }
}

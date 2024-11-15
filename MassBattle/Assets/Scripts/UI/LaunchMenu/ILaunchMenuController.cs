using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.Databases.ArmyDatabase;
using MassBattle.Logic.Databases.Colors;
using MassBattle.Logic.Databases.UnitDatabase;

namespace MassBattle.UI.LaunchMenu
{
    public interface ILaunchMenuController
    {
        void InjectData(
                IArmyDatabase armyDatabase, IColorDatabase colorDatabase, ISceneLoader sceneLoader,
                IExitGameProvider exitGameProvider, IUnitDatabase unitDatabase);
    }
}

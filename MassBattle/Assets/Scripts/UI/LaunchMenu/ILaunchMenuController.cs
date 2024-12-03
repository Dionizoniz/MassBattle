using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.Databases.Armies;
using MassBattle.Logic.Databases.Colors;
using MassBattle.Logic.Databases.Units;

namespace MassBattle.UI.LaunchMenu
{
    public interface ILaunchMenuController
    {
        void InjectData(
                IArmyDatabase armyDatabase, IColorDatabase colorDatabase, ISceneLoader sceneLoader,
                IUnitDatabase unitDatabase);
    }
}

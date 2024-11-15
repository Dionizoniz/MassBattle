using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.Armies;
using MassBattle.Logic.Databases.ArmyDatabase;
using MassBattle.Logic.Databases.Colors;
using MassBattle.Logic.Databases.UnitDatabase;
using MassBattle.Logic.Providers;

namespace MassBattle.Logic.BattleCreator
{
    public interface IBattleSpawner
    {
        void Initialize(
                IArmyDatabase armyDatabase, IArmyProvider armyProvider, IUpdateProvider updateProvider,
                IUnitsFactory unitsFactory, IColorDatabase colorDatabase, ISceneLoader sceneLoader,
                IUnitDatabase unitDatabase);
    }
}

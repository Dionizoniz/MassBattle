using MassBattle.Logic.Armies;
using MassBattle.Logic.Providers;

namespace MassBattle.Logic.BattleCreator
{
    public interface IBattleSpawner
    {
        void Initialize(
                IBattleSetup battleSetup, IArmyProvider armyProvider, IUpdateProvider updateProvider,
                IUnitsFactory unitsFactory);
    }
}

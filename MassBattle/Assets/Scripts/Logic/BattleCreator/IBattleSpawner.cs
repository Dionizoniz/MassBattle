using MassBattle.Logic.Armies;
using MassBattle.Logic.Setup;

namespace MassBattle.Logic.BattleCreator
{
    public interface IBattleSpawner
    {
        void Initialize(IBattleSetup battleSetup, IArmyProvider armyProvider);
    }
}

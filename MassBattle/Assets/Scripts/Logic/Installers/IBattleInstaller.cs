using MassBattle.Logic.Armies;
using MassBattle.Logic.BattleCreator;
using MassBattle.Logic.Setup;

namespace MassBattle.Logic.Installers
{
    public interface IBattleInstaller
    {
        IBattleSpawner BattleSpawner { get; }
        IBattleSetup BattleSetup { get; }

        IArmyProvider ArmyProvider { get; }
    }
}

using MassBattle.Logic.Armies;
using MassBattle.Logic.BattleCreator;

namespace MassBattle.Logic.Installers
{
    public interface IBattleInstaller
    {
        IBattleSpawner BattleSpawner { get; }
        IArmyProvider ArmyProvider { get; }
    }
}

using MassBattle.Logic.Armies;

namespace MassBattle.Logic.Installers
{
    public interface IBattleInstaller
    {
        IArmyProvider ArmyProvider { get; }
    }
}

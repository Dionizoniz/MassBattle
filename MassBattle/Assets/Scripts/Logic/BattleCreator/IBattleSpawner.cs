using MassBattle.Logic.Installers;

namespace MassBattle.Logic.BattleCreator
{
    public interface IBattleSpawner : IInitialize
    {
        Army Army1 { get; }
        Army Army2 { get; }
    }
}

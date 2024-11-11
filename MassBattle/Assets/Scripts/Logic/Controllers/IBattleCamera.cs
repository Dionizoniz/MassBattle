using MassBattle.Logic.Armies;

namespace MassBattle.Logic.Controllers
{
    public interface IBattleCamera
    {
        void Initialize(IArmyProvider armyProvider);
    }
}

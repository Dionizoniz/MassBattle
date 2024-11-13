using MassBattle.Core.Providers;
using MassBattle.Core.UserInput;
using MassBattle.Logic.Armies;
using MassBattle.Logic.Providers;

namespace MassBattle.Logic.Controllers
{
    public interface IBattleCamera
    {
        void Initialize(IArmyProvider armyProvider, IUpdateProvider updateProvider, IInputFacade inputFacade);
    }
}

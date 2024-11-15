using MassBattle.Core.Providers;
using MassBattle.Core.UserInput;
using MassBattle.Logic.Armies;

namespace MassBattle.Logic.Controllers
{
    public interface IBattleCamera
    {
        void InjectData(IArmyProvider armyProvider, IUpdateProvider updateProvider, IInputFacade inputFacade);
    }
}

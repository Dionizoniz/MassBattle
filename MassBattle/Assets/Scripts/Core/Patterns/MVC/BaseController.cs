using MassBattle.Core.Engine;
using MassBattle.Core.Entities;

namespace MassBattle.Core.Patterns.MVC
{
    public abstract class BaseController : ExtendedMonoBehaviour, ICheckSetup
    {
        public abstract bool IsSetupCorrect();
    }
}

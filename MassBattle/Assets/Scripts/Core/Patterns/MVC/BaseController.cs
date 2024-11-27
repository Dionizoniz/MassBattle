using MassBattle.Core.Entities.Engine;
using MassBattle.Core.Entities.Tests;

namespace MassBattle.Core.Entities.MVC
{
    public abstract class BaseController : ExtendedMonoBehaviour, ICheckSetup
    {
        public abstract bool IsSetupCorrect();
    }
}

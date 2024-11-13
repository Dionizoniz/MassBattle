using MassBattle.Core.Entities.Engine;
using MassBattle.Core.Entities.Tests;

namespace MassBattle.Core.Entities.Installers
{
    public abstract class BaseInstaller : ExtendedMonoBehaviour, ICheckSetup
    {
        public abstract bool IsSetupCorrect();
    }
}

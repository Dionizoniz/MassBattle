using MassBattle.Core.Entities.Tests;

namespace MassBattle.Core.Entities.Engine
{
    public abstract class BaseSceneEntity : ExtendedMonoBehaviour, ICheckSetup
    {
        public abstract bool IsSetupCorrect();
    }
}

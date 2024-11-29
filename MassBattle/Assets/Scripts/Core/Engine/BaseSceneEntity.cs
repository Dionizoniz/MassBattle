using MassBattle.Core.Entities;

namespace MassBattle.Core.Engine
{
    public abstract class BaseSceneEntity : ExtendedMonoBehaviour, ICheckSetup
    {
        public abstract bool IsSetupCorrect();
    }
}

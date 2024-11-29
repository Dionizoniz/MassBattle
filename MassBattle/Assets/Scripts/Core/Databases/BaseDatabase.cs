using MassBattle.Core.Engine;
using MassBattle.Core.Entities;

namespace MassBattle.Core.Databases
{
    public abstract class BaseDatabase : ExtendedScriptableObject, ICheckSetup
    {
        public abstract bool IsSetupCorrect();
    }
}

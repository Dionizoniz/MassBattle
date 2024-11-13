using MassBattle.Core.Entities.Engine;
using MassBattle.Core.Entities.Tests;

namespace MassBattle.Core.Entities.Database
{
    public abstract class BaseDatabase : ExtendedScriptableObject, ICheckSetup
    {
        public abstract bool IsSetupCorrect();
    }
}

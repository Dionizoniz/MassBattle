using System;
using MassBattle.Core.Entities.Engine;
using MassBattle.Core.Entities.Tests;

namespace MassBattle.Core.Entities.Installers
{
    public abstract class BaseInstaller : ExtendedMonoBehaviour, IBaseBattleInstaller, ICheckSetup
    {
        public event Action OnSpawnFinish = delegate { };

        protected virtual void Start()
        {
            NotifyOnSpawnFinish();
        }

        private void NotifyOnSpawnFinish()
        {
            OnSpawnFinish.Invoke();
        }

        public abstract bool IsSetupCorrect();
    }
}

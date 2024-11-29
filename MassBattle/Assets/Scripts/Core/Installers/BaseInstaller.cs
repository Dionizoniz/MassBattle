using System;
using MassBattle.Core.Engine;
using MassBattle.Core.Entities;

namespace MassBattle.Core.Installers
{
    public abstract class BaseInstaller : ExtendedMonoBehaviour, IInstaller, ICheckSetup
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

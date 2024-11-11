using System;
using MassBattle.Core.Entities.Engine;

namespace MassBattle.Logic.Providers
{
    public class LifeCycleProvider : ExtendedMonoBehaviour, ILifeCycleProvider
    {
        public event Action OnUpdate = delegate
                                       { };

        private void Update()
        {
            OnUpdate.Invoke();
        }
    }
}

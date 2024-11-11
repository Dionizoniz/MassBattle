using System;
using MassBattle.Core.Entities.Engine;

namespace MassBattle.Logic.Providers
{
    public class UpdateProvider : ExtendedMonoBehaviour, IUpdateProvider
    {
        public event Action OnUpdate = delegate
                                       { };

        private void Update()
        {
            OnUpdate.Invoke();
        }
    }
}

using MassBattle.Core.Installers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MassBattle.DependencyInjection.Installers.Systems
{
    public class EventSystemInstaller : ExtendedMonoInstaller
    {
        [SerializeField]
        private EventSystem _eventSystem;

        public override void InstallBindings()
        {
            BindFromComponentInNewPrefab(_eventSystem);
        }
    }
}

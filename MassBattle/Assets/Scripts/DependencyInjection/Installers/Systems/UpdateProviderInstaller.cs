using MassBattle.Core.Installers;
using MassBattle.Core.Providers;
using UnityEngine;

namespace MassBattle.DependencyInjection.Installers.Systems
{
    public class UpdateProviderInstaller : ExtendedMonoInstaller
    {
        [SerializeField]
        private UpdateProvider _updateProvider;

        public override void InstallBindings()
        {
            BindInterfacesToFromComponentInNewPrefab(_updateProvider);
        }

        public override bool IsSetupCorrect() => _updateProvider;
    }
}

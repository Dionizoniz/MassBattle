using MassBattle.Core.Installers;
using UnityEngine;

namespace MassBattle.DependencyInjection.Installers.Systems
{
    public class CameraInstaller : ExtendedMonoInstaller
    {
        [SerializeField]
        private Camera _camera;

        public override void InstallBindings()
        {
            BindFromComponentInNewPrefab(_camera);
        }

        public override bool IsSetupCorrect() => _camera != null;
    }
}

using MassBattle.Core.Installers;
using MassBattle.Logic.Controllers;
using UnityEngine;

namespace MassBattle.DependencyInjection.Installers.Systems
{
    public class BattleCameraInstaller : ExtendedMonoInstaller
    {
        [SerializeField]
        private BattleCamera _battleCamera;

        public override void InstallBindings()
        {
            BindFromComponentInNewPrefab(_battleCamera);
        }

        public override bool IsSetupCorrect() => _battleCamera != null;
    }
}

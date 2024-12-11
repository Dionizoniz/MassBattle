using MassBattle.Core.Installers;
using MassBattle.Logic.Databases.Armies;
using UnityEngine;

namespace MassBattle.DependencyInjection.Installers.Databases
{
    public class BattleSetupInstaller : ExtendedMonoInstaller
    {
        [SerializeField]
        private BattleSetup _battleSetup;

        public override void InstallBindings()
        {
            BindInterfacesToFromInstance(_battleSetup);
        }

        public override bool IsSetupCorrect() => _battleSetup != null;
    }
}

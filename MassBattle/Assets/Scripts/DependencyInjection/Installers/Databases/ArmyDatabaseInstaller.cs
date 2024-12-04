using MassBattle.Core.Installers;
using MassBattle.Logic.Databases.Armies;
using UnityEngine;

namespace MassBattle.DependencyInjection.Installers.Databases
{
    public class ArmyDatabaseInstaller : ExtendedMonoInstaller
    {
        [SerializeField]
        private ArmyDatabase _armyDatabase;

        public override void InstallBindings()
        {
            BindInterfacesToFromInstance(_armyDatabase);
        }
    }
}

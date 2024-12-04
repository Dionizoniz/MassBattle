using MassBattle.Core.Installers;
using MassBattle.Logic.Databases.Units;
using UnityEngine;

namespace MassBattle.DependencyInjection.Installers.Databases
{
    public class UnitDatabaseInstaller : ExtendedMonoInstaller
    {
        [SerializeField]
        private UnitDatabase _unitDatabase;

        public override void InstallBindings()
        {
            BindInterfacesToFromInstance(_unitDatabase);
        }
    }
}

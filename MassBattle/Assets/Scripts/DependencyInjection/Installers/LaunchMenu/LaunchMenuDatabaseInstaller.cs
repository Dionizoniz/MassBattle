using MassBattle.Core.Installers;
using MassBattle.Logic.Databases.Armies;
using MassBattle.Logic.Databases.Colors;
using MassBattle.Logic.Databases.Units;
using UnityEngine;

namespace MassBattle.DependencyInjection.Installers.LaunchMenu
{
    public class LaunchMenuDatabaseInstaller : ExtendedMonoInstaller
    {
        [Space, SerializeField]
        private ArmyDatabase _armyDatabase;
        [SerializeField]
        private ColorDatabase _colorDatabase;
        [SerializeField]
        private UnitDatabase _unitDatabase;

        public override void InstallBindings()
        {
            BindInterfacesToFromInstance(_armyDatabase);
            BindInterfacesToFromInstance(_colorDatabase);
            BindInterfacesToFromInstance(_unitDatabase);
        }
    }
}

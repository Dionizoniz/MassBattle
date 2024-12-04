using MassBattle.Core.Installers;
using MassBattle.Logic.Databases.Colors;
using UnityEngine;

namespace MassBattle.DependencyInjection.Installers.Databases
{
    public class ColorDatabaseInstaller : ExtendedMonoInstaller
    {
        [SerializeField]
        private ColorDatabase _colorDatabase;

        public override void InstallBindings()
        {
            BindInterfacesToFromInstance(_colorDatabase);
        }
    }
}

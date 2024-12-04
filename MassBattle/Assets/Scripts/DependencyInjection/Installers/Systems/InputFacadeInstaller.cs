using MassBattle.Core.Installers;
using MassBattle.Core.UserInput;
using UnityEngine;

namespace MassBattle.DependencyInjection.Installers.Systems
{
    public class InputFacadeInstaller : ExtendedMonoInstaller
    {
        [SerializeField]
        private InputFacade _inputFacade;

        public override void InstallBindings()
        {
            BindInterfacesToFromComponentInNewPrefab(_inputFacade);
        }

        public override bool IsSetupCorrect() => _inputFacade != null;
    }
}

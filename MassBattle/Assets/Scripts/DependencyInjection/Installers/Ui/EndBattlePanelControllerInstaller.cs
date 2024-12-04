using MassBattle.Core.Installers;
using MassBattle.UI.EndBattlePanel;
using UnityEngine;

namespace MassBattle.DependencyInjection.Installers.Ui
{
    public class EndBattlePanelControllerInstaller : ExtendedMonoInstaller
    {
        [SerializeField]
        private EndBattlePanelController _endBattlePanelController;

        public override void InstallBindings()
        {
            BindInterfacesToFromComponentInNewPrefab(_endBattlePanelController);
        }

        public override bool IsSetupCorrect() => _endBattlePanelController != null;
    }
}

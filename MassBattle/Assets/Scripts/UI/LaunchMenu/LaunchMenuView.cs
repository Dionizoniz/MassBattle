using System.Collections.Generic;
using MassBattle.Core.Patterns;
using MassBattle.Logic.Setup;
using UnityEngine;

namespace MassBattle.UI.LaunchMenu
{
    public class LaunchMenuView : View
    {
        [SerializeField]
        private ArmyPanelController _armyPanelToSpawn;
        [SerializeField]
        private RectTransform _armyPanelsRoot;

        public List<ArmyPanelController> ArmyPanels { get; } = new();

        public void InitializePanels(BattleSetup battleSetup) // TODO change to SpawnPanels and adjust logic
        {
            foreach (var armyId in battleSetup.FindAllArmySetupIds())
            {
                ArmySetup armySetup = battleSetup.TryFindArmySetupBy(armyId);

                if (armySetup != null)
                {
                    SpawnArmyPanel(armySetup);
                }
            }
        }

        private void SpawnArmyPanel(ArmySetup armySetup)
        {
            ArmyPanelController armyPanel = Instantiate(_armyPanelToSpawn, _armyPanelsRoot);
            armyPanel.InitializeData(armySetup);

            ArmyPanels.Add(armyPanel);
        }
    }
}

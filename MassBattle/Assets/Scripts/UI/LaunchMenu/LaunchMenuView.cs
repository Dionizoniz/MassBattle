using System.Collections.Generic;
using MassBattle.Core.Patterns;
using MassBattle.Logic.Databases;
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

        // TODO change to SpawnPanels and adjust logic
        public void InitializePanels(BattleSetup battleSetup, IColorDatabase colorDatabase)
        {
            foreach (var armyId in battleSetup.FindAllArmySetupIds())
            {
                ArmySetup armySetup = battleSetup.TryFindArmySetupBy(armyId);

                if (armySetup != null)
                {
                    SpawnArmyPanel(armySetup, colorDatabase);
                }
            }
        }

        private void SpawnArmyPanel(ArmySetup armySetup, IColorDatabase colorDatabase)
        {
            ArmyPanelController armyPanel = Instantiate(_armyPanelToSpawn, _armyPanelsRoot);
            armyPanel.InitializeData(armySetup, colorDatabase);

            ArmyPanels.Add(armyPanel);
        }
    }
}

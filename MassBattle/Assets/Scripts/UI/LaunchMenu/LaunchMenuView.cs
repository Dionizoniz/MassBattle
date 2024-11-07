using System.Collections.Generic;
using MassBattle.Core.Patterns;
using MassBattle.Logic.Setup;
using UnityEngine;

namespace MassBattle.UI.LaunchMenu
{
    public class LaunchMenuView : View
    {
        [SerializeField]
        private List<ArmyPanelController> _armyPanels = new();

        public List<ArmyPanelController> ArmyPanels => _armyPanels;

        public void InitializePanels(BattleSetup battleSetup) // TODO change to SpawnPanels and adjust logic
        {
            List<string> armyIds = battleSetup.FindAllArmySetupIds();

            for (var i = 0; i < armyIds.Count || i < ArmyPanels.Count; i++)
            {
                ArmySetup armySetup = battleSetup.TryFindArmySetupBy(armyIds[i]);
                ArmyPanels[i].InitializeData(armySetup);
            }
        }
    }
}

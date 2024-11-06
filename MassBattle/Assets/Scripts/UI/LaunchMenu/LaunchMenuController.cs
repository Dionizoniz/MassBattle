using System.Collections.Generic;
using MassBattle.Logic.Setup;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MassBattle.UI.LaunchMenu
{
    public class LaunchMenuController : MonoBehaviour
    {
        [SerializeField]
        private BattleSetup battleSetup;

        [SerializeField]
        private List<ArmyPanelController> armyPanels = new();

        public void StartBattle()
        {
            battleSetup.ClearRegisteredArmySetups();

            foreach (var panel in armyPanels)
            {
                ArmySetup armySetup = panel.CreateArmySetup();
                battleSetup.RegisterArmySetup(armySetup);
            }

            SceneManager.LoadScene(1);
        }

        private void Awake()
        {
            List<string> armyIds = battleSetup.FindAllArmySetupIds();

            for (var i = 0; i < armyIds.Count || i < armyPanels.Count; i++)
            {
                ArmySetup armySetup = battleSetup.TryFindArmySetupBy(armyIds[i]);
                armyPanels[i].InitializeData(armySetup);
            }
        }
    }
}

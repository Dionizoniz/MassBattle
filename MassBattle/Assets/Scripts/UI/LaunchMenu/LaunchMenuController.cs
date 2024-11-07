using System.Collections.Generic;
using MassBattle.Logic.Setup;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MassBattle.UI.LaunchMenu
{
    public class LaunchMenuController : MonoBehaviour
    {
        [SerializeField]
        private BattleSetup _battleSetup;

        [SerializeField]
        private List<ArmyPanelController> _armyPanels = new();

        public void StartBattle()
        {
            ClearRegisteredArmySetups();
            RegisterArmiesSetup();
            LoadBattleScene();
        }

        private void ClearRegisteredArmySetups()
        {
            _battleSetup.ClearRegisteredArmySetups();
        }

        private void RegisterArmiesSetup()
        {
            foreach (var panel in _armyPanels)
            {
                ArmySetup armySetup = panel.CreateArmySetup();
                _battleSetup.RegisterArmySetup(armySetup);
            }
        }

        private void LoadBattleScene()
        {
            SceneManager.LoadScene(1);
        }

        private void Awake()
        {
            InitializePanels();
        }

        private void InitializePanels()
        {
            List<string> armyIds = _battleSetup.FindAllArmySetupIds();

            for (var i = 0; i < armyIds.Count || i < _armyPanels.Count; i++)
            {
                ArmySetup armySetup = _battleSetup.TryFindArmySetupBy(armyIds[i]);
                _armyPanels[i].InitializeData(armySetup);
            }
        }
    }
}

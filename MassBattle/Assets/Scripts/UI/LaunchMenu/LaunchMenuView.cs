using System.Collections.Generic;
using MassBattle.Core.Entities.MVC;
using MassBattle.Logic.Armies;
using MassBattle.Logic.BattleCreator;
using MassBattle.Logic.Databases;
using MassBattle.Logic.Databases.Colors;
using TMPro;
using UnityEngine;

namespace MassBattle.UI.LaunchMenu
{
    public class LaunchMenuView : View
    {
        private const string ARMY_ID_DUPLICATES_ERROR_MESSAGE = "Remove duplicates in Army Names before Launch Battle.";

        [SerializeField]
        private ArmyPanelController _armyPanelToSpawn;
        [SerializeField]
        private RectTransform _armyPanelsRoot;

        [Space, SerializeField]
        private GameObject _errorMessageRoot;
        [SerializeField]
        private TextMeshProUGUI _errorMessageLabel;

        [Space, SerializeField]
        private GameObject _exitScreenRoot;

        public List<ArmyPanelController> ArmyPanels { get; } = new();

        private void Awake()
        {
            HideErrorMessage();
            HideExitPanel();
        }

        private void HideErrorMessage()
        {
            _errorMessageRoot.gameObject.SetActive(false);
        }

        private void HideExitPanel()
        {
            _exitScreenRoot.gameObject.SetActive(false);
        }

        public void SpawnPanels(IBattleSetup battleSetup, IColorDatabase colorDatabase)
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

        public void ShowArmyIdsErrorMessage()
        {
            ShowErrorMessage(ARMY_ID_DUPLICATES_ERROR_MESSAGE);
        }

        private void ShowErrorMessage(string errorMessage)
        {
            _errorMessageLabel.text = errorMessage;
            _errorMessageRoot.gameObject.SetActive(true);
        }

        public void ShowExitPanel()
        {
            _exitScreenRoot.gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            DestroyArmyPanels();
        }

        private void DestroyArmyPanels()
        {
            foreach (var panel in ArmyPanels)
            {
                Destroy(panel._gameObject);
            }

            ArmyPanels.Clear();
        }
    }
}

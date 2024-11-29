using System.Collections.Generic;
using MassBattle.Core.Patterns.MVC;
using MassBattle.Logic.Databases.Armies;
using MassBattle.Logic.Databases.Colors;
using MassBattle.Logic.Databases.Units;
using TMPro;
using UnityEngine;

namespace MassBattle.UI.LaunchMenu
{
    public class LaunchMenuView : View
    {
        private const string ARMY_NAME_DUPLICATES_ERROR_MESSAGE =
                "Remove duplicates in army NAMES before starting to make it easier to identify an army.";

        private const string ARMY_COLOR_DUPLICATES_ERROR_MESSAGE =
                "Remove duplicates in army COLORS before starting to make it easier to identify an army.";

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

        public void SpawnPanels(IArmyDatabase armyDatabase, IColorDatabase colorDatabase, IUnitDatabase unitDatabase)
        {
            foreach (InitialArmyData initialArmyData in armyDatabase.ArmiesData)
            {
                SpawnArmyPanel(initialArmyData, colorDatabase, unitDatabase, armyDatabase);
            }
        }

        private void SpawnArmyPanel(
                InitialArmyData initialArmyData, IColorDatabase colorDatabase, IUnitDatabase unitDatabase,
                IArmyDatabase armyDatabase)
        {
            ArmyPanelController armyPanel = Instantiate(_armyPanelToSpawn, _armyPanelsRoot);
            armyPanel.InitializeData(initialArmyData, colorDatabase, unitDatabase, armyDatabase);

            ArmyPanels.Add(armyPanel);
        }

        public void ShowArmyNamesErrorMessage()
        {
            ShowErrorMessage(ARMY_NAME_DUPLICATES_ERROR_MESSAGE);
        }

        public void ShowArmyColorsErrorMessage()
        {
            ShowErrorMessage(ARMY_COLOR_DUPLICATES_ERROR_MESSAGE);
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

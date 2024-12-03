using System.Collections.Generic;
using MassBattle.Core.Patterns.MVC;
using MassBattle.Logic.Databases.Armies;
using MassBattle.Logic.Databases.Colors;
using MassBattle.Logic.Databases.Units;
using TMPro;
using UnityEngine;
using Zenject;

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

        private IArmyDatabase _armyDatabase;
        private IColorDatabase _colorDatabase;
        private IUnitDatabase _unitDatabase;

        public List<ArmyPanelController> ArmyPanels { get; } = new();

        [Inject]
        private void Construct(IArmyDatabase armyDatabase, IColorDatabase colorDatabase, IUnitDatabase unitDatabase)
        {
            _armyDatabase = armyDatabase;
            _colorDatabase = colorDatabase;
            _unitDatabase = unitDatabase;
        }

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

        public void SpawnPanels()
        {
            foreach (InitialArmyData initialArmyData in _armyDatabase.ArmiesData)
            {
                SpawnArmyPanel(initialArmyData);
            }
        }

        private void SpawnArmyPanel(InitialArmyData initialArmyData)
        {
            ArmyPanelController armyPanel = Instantiate(_armyPanelToSpawn, _armyPanelsRoot);
            armyPanel.InitializeData(initialArmyData, _colorDatabase, _unitDatabase, _armyDatabase);

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

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
        // TODO move to const's class
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

        private IArmyDatabase _armyDatabase;
        private DiContainer _container;

        [Inject]
        private void Construct(IArmyDatabase armyDatabase, DiContainer container)
        {
            _armyDatabase = armyDatabase;
            _container = container;
        }

        private void Awake()
        {
            HideErrorMessage();
            HideExitPanel();
            SpawnPanels();
        }

        private void HideErrorMessage()
        {
            _errorMessageRoot.gameObject.SetActive(false);
        }

        private void HideExitPanel()
        {
            _exitScreenRoot.gameObject.SetActive(false);
        }

        private void SpawnPanels()
        {
            foreach (InitialArmyData initialArmyData in _armyDatabase.ArmiesData)
            {
                SpawnArmyPanel(initialArmyData);
            }
        }

        private void SpawnArmyPanel(InitialArmyData initialArmyData)
        {
            ArmyPanelController armyPanel = Instantiate(_armyPanelToSpawn, _armyPanelsRoot);
            _container.InjectGameObject(armyPanel.gameObject);
            armyPanel.InitializeData(initialArmyData);

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

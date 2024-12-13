using System.Collections.Generic;
using MassBattle.Core.Patterns.MVC;
using MassBattle.Core.Providers;
using MassBattle.Logic.Battle.Setup;
using TMPro;
using UnityEngine;
using Zenject;

namespace MassBattle.UI.LaunchMenu
{
    public class LaunchMenuView : View
    {
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

        private IBattleSetup _battleSetup;
        private DiContainer _container;

        [Inject]
        private void Construct(IBattleSetup battleSetup, DiContainer container)
        {
            _battleSetup = battleSetup;
            _container = container;
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
            // TODO: improve spawning for empty list
            foreach (InitialArmyData initialArmyData in _battleSetup.SavedArmiesData)
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
            ShowErrorMessage(ConstantValues.ARMY_NAME_DUPLICATES_ERROR_MESSAGE);
        }

        public void ShowArmyColorsErrorMessage()
        {
            ShowErrorMessage(ConstantValues.ARMY_COLOR_DUPLICATES_ERROR_MESSAGE);
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

using MassBattle.Core.Entities.Engine;
using MassBattle.Core.Utilities;
using MassBattle.Logic.Armies;
using MassBattle.Logic.Databases;
using MassBattle.Logic.Strategies;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MassBattle.UI.LaunchMenu
{
    public class ArmyPanelController : ExtendedMonoBehaviour
    {
        [SerializeField]
        private TMP_InputField _armyIdInputField;
        [SerializeField]
        private Toggle _isArmyActiveToggle;

        [Space, SerializeField]
        private Slider _warriorsSlider;
        [SerializeField]
        private TextMeshProUGUI _warriorsCountLabel;

        [Space, SerializeField]
        private Slider _archerSlider;
        [SerializeField]
        private TextMeshProUGUI _archerCountLabel;

        [Space, SerializeField]
        private TMP_Dropdown _strategyDropdown;
        [SerializeField]
        private Image _armyColor;

        public string ArmyId => _armyIdInputField.text;

        private EnumDropdownWrapper<StrategyType> _strategyTypeWrapper;
        private IColorDatabase _colorDatabase;

        public void InitializeData(ArmySetup armySetup, IColorDatabase colorDatabase)
        {
            _colorDatabase = colorDatabase;

            _armyIdInputField.text = armySetup.ArmyId;
            _isArmyActiveToggle.isOn = armySetup.IsArmyActive;
            _warriorsSlider.value = armySetup.WarriorsCount;
            _archerSlider.value = armySetup.ArchersCount;
            _armyColor.color = armySetup.ArmyColor;

            _strategyTypeWrapper = new EnumDropdownWrapper<StrategyType>(_strategyDropdown);
            _strategyDropdown.SetValueWithoutNotify((int)armySetup.StrategyType);

            ForceRefreshArmyCountLabels();
        }

        private void ForceRefreshArmyCountLabels() // INFO: if SetupValue is same as SliderValue - labels do not refresh
        {
            RefreshWarriorsCountLabel(_warriorsSlider.value);
            RefreshArchersCountLabel(_archerSlider.value);
        }

        public void RefreshWarriorsCountLabel(float value)
        {
            _warriorsCountLabel.text = value.ToString();
        }

        public void RefreshArchersCountLabel(float value)
        {
            _archerCountLabel.text = value.ToString();
        }

        public ArmySetup CreateArmySetup()
        {
            int warriorsCount = (int)_warriorsSlider.value;
            int archersCount = (int)_archerSlider.value;
            StrategyType strategyType = _strategyTypeWrapper.Value();
            Color armyColor = _armyColor.color;
            bool isArmyActive = _isArmyActiveToggle.isOn;

            return new ArmySetup(ArmyId, warriorsCount, archersCount, strategyType, armyColor, isArmyActive);
        }

        public void ChangeArmyColorToNext()
        {
            int index = _colorDatabase.FindColorIndex(_armyColor.color);
            ColorData nextColor = FindNextColorData(index);

            _armyColor.color = nextColor.Color;
        }

        private ColorData FindNextColorData(int index)
        {
            return index >= 0 ? _colorDatabase.TryFindNextElementFor(index) : _colorDatabase.FindDefaultElement();
        }

        private void OnDestroy()
        {
            _strategyTypeWrapper.Dispose();
        }
    }
}

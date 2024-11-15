using System;
using System.Collections.Generic;
using MassBattle.Core.Entities.Engine;
using MassBattle.Core.Utilities;
using MassBattle.Logic.Armies;
using MassBattle.Logic.Databases;
using MassBattle.Logic.Databases.ArmyDatabase;
using MassBattle.Logic.Databases.Colors;
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

        public string ArmyName => _armyIdInputField.text;

        private EnumDropdownWrapper<StrategyType> _strategyTypeWrapper;
        private IColorDatabase _colorDatabase;

        private string _cachedId; // TODO improve

        public void InitializeData(InitialArmyData initialArmyData, IColorDatabase colorDatabase)
        {
            _cachedId = initialArmyData.Id;
            _colorDatabase = colorDatabase;

            _armyIdInputField.text = initialArmyData.ArmyName;
            _isArmyActiveToggle.isOn = initialArmyData.IsArmyActive;
            _warriorsSlider.value = initialArmyData.DefaultUnitStackSize;
            _archerSlider.value = initialArmyData.DefaultUnitStackSize;
            _armyColor.color = initialArmyData.ArmyColor;

            _strategyTypeWrapper = new EnumDropdownWrapper<StrategyType>(_strategyDropdown);
            _strategyDropdown.SetValueWithoutNotify((int)initialArmyData.StrategyType);

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

        public InitialArmyData CreateArmySetup(int index)
        {
            int warriorsCount = (int)_warriorsSlider.value;
            int archersCount = (int)_archerSlider.value;
            StrategyType strategyType = _strategyTypeWrapper.Value();
            Color armyColor = _armyColor.color;
            bool isArmyActive = _isArmyActiveToggle.isOn;

            // TODO improve units count logic
            Dictionary<string, int> units = new Dictionary<string, int>();
            units.Add("0", warriorsCount);
            units.Add("1", archersCount);

            return new InitialArmyData(_cachedId, ArmyName, units, strategyType, armyColor, isArmyActive);
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

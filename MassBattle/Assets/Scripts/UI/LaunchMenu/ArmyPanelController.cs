using System;
using System.Collections.Generic;
using MassBattle.Core.Entities.Engine;
using MassBattle.Core.Utilities;
using MassBattle.Logic.Armies;
using MassBattle.Logic.Databases;
using MassBattle.Logic.Databases.ArmyDatabase;
using MassBattle.Logic.Databases.Colors;
using MassBattle.Logic.Databases.UnitDatabase;
using MassBattle.Logic.Strategies;
using MassBattle.UI.Components;
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
        private UnitsCountSliderController _unitCountSlidersToSpawn;
        [SerializeField]
        private Transform _unitCountSlidersRoot;

        [Space, SerializeField]
        private TMP_Dropdown _strategyDropdown;
        [SerializeField]
        private Image _armyColor;

        public string ArmyName => _armyIdInputField.text;
        public bool IsArmyActive => _isArmyActiveToggle.isOn;

        private EnumDropdownWrapper<StrategyType> _strategyTypeWrapper;
        private IColorDatabase _colorDatabase;
        private IUnitDatabase _unitDatabase;

        private string _armyId;
        private readonly List<UnitsCountSliderController> _spawnedUnitsSliders = new();

        public void InitializeData(
                InitialArmyData initialArmyData, IColorDatabase colorDatabase, IUnitDatabase unitDatabase)
        {
            _armyId = initialArmyData.Id;
            _colorDatabase = colorDatabase;
            _unitDatabase = unitDatabase;

            _armyIdInputField.text = initialArmyData.ArmyName;
            _isArmyActiveToggle.isOn = initialArmyData.IsArmyActive;
            _armyColor.color = initialArmyData.ArmyColor;

            _strategyTypeWrapper = new EnumDropdownWrapper<StrategyType>(_strategyDropdown);
            _strategyDropdown.SetValueWithoutNotify((int)initialArmyData.StrategyType);

            SpawnUnitsSliders(initialArmyData);
        }

        private void SpawnUnitsSliders(InitialArmyData initialArmyData)
        {
            foreach (var unitSetup in FindUnitsCountSetup(initialArmyData))
            {
                UnitsCountSliderController spawnedSlider = Instantiate(_unitCountSlidersToSpawn, _unitCountSlidersRoot);

                UnitData unitData = _unitDatabase.TryFindElementBy(unitSetup.Key);
                spawnedSlider.Initialize(unitData, unitSetup.Value);
                _spawnedUnitsSliders.Add(spawnedSlider);
            }
        }

        private Dictionary<string, int> FindUnitsCountSetup(InitialArmyData initialArmyData)
        {
            Dictionary<string, int> setup = initialArmyData.UnitsCountSetup;
            return setup ?? _unitDatabase.GenerateDefaultUnitsCountSetup(initialArmyData.DefaultUnitStackSize);
        }

        public InitialArmyData CreateArmySetup()
        {
            StrategyType strategyType = _strategyTypeWrapper.Value();
            Color armyColor = _armyColor.color;
            bool isArmyActive = _isArmyActiveToggle.isOn;
            Dictionary<string, int> unitCountSliders = new();

            foreach (var slider in _spawnedUnitsSliders)
            {
                unitCountSliders.Add(slider.UnitId, slider.UnitsCount);
            }

            return new InitialArmyData(_armyId, ArmyName, unitCountSliders, strategyType, armyColor, isArmyActive);
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

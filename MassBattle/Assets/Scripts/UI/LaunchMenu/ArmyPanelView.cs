using System.Collections.Generic;
using MassBattle.Core.Patterns.MVC;
using MassBattle.Core.Utilities;
using MassBattle.Logic.Databases.Armies;
using MassBattle.Logic.Databases.Colors;
using MassBattle.Logic.Databases.Units;
using MassBattle.Logic.Strategies;
using MassBattle.UI.Components;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MassBattle.UI.LaunchMenu
{
    public class ArmyPanelView : View
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
        public Color ArmyColor => _armyColor.color;
        public StrategyType StrategyType => _strategyTypeWrapper.Value();
        public string ArmyId { get; private set; }

        private IArmyDatabase _armyDatabase;
        private IColorDatabase _colorDatabase;
        private IUnitDatabase _unitDatabase;

        private EnumDropdownWrapper<StrategyType> _strategyTypeWrapper;
        private readonly List<UnitsCountSliderController> _spawnedUnitsSliders = new();

        [Inject]
        private void Construct(IArmyDatabase armyDatabase, IColorDatabase colorDatabase, IUnitDatabase unitDatabase)
        {
            _armyDatabase = armyDatabase;
            _colorDatabase = colorDatabase;
            _unitDatabase = unitDatabase;
        }

        public void InitializeData(InitialArmyData initialArmyData)
        {
            ArmyId = initialArmyData.DescriptorId;
            _armyIdInputField.text = initialArmyData.Name;
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
                UnitDescriptor unitDescriptor = _unitDatabase.TryFindElementBy(unitSetup.Key);
                UnitsCountSliderController spawnedSlider = Instantiate(_unitCountSlidersToSpawn, _unitCountSlidersRoot);
                
                spawnedSlider.Initialize(unitDescriptor, unitSetup.Value, _armyDatabase.UnitStackSizeRange);
                _spawnedUnitsSliders.Add(spawnedSlider);
            }
        }

        private Dictionary<string, int> FindUnitsCountSetup(InitialArmyData initialArmyData)
        {
            Dictionary<string, int> setup = initialArmyData.UnitsCountSetup;
            return setup ?? _unitDatabase.GenerateDefaultUnitsCountSetup(initialArmyData.DefaultUnitStackSize);
        }

        public void ChangeArmyColorToNext()
        {
            int index = _colorDatabase.FindColorIndex(_armyColor.color);
            ColorDescriptor nextColor = FindNextColorData(index);

            _armyColor.color = nextColor.Color;
        }

        private ColorDescriptor FindNextColorData(int index)
        {
            return index >= 0 ? _colorDatabase.TryFindNextElementFor(index) : _colorDatabase.FindDefaultElement();
        }

        public Dictionary<string, int> FindUnitsCountSetup()
        {
            Dictionary<string, int> unitsCountSetup = new();

            foreach (var slider in _spawnedUnitsSliders)
            {
                unitsCountSetup.Add(slider.UnitId, slider.UnitsCount);
            }

            return unitsCountSetup;
        }

        private void OnDestroy()
        {
            _strategyTypeWrapper.Dispose();
        }
    }
}

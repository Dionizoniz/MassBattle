using System.Collections.Generic;
using System.Linq;
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

        private IBattleSetup _battleSetup;
        private IColorDatabase _colorDatabase;
        private IUnitDatabase _unitDatabase;

        private EnumDropdownWrapper<StrategyType> _strategyTypeWrapper;
        private readonly List<UnitsCountSliderController> _spawnedUnitsSliders = new();

        [Inject]
        private void Construct(IBattleSetup battleSetup, IColorDatabase colorDatabase, IUnitDatabase unitDatabase)
        {
            _battleSetup = battleSetup;
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

                spawnedSlider.Initialize(unitDescriptor, unitSetup.Value, _battleSetup.UnitStackSizeRange);
                _spawnedUnitsSliders.Add(spawnedSlider);
            }
        }

        private Dictionary<string, int> FindUnitsCountSetup(InitialArmyData initialArmyData)
        {
            return initialArmyData.UnitsCountSetup ?? _unitDatabase.GenerateDefaultUnitsCountSetup(initialArmyData);
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
            return _spawnedUnitsSliders.ToDictionary(slider => slider.UnitId, slider => slider.UnitsCount);
        }

        private void OnDestroy()
        {
            _strategyTypeWrapper.Dispose();
        }
    }
}

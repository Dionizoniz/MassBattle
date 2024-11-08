using MassBattle.Core.Utilities;
using MassBattle.Logic.Databases;
using MassBattle.Logic.Setup;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MassBattle.UI.LaunchMenu
{
    public class ArmyPanelController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _armyIdLabel;

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

        private EnumDropdownWrapper<StrategyType> _strategyTypeWrapper;
        private IColorDatabase _colorDatabase;

        public void InitializeData(ArmySetup armySetup, IColorDatabase colorDatabase)
        {
            _colorDatabase = colorDatabase;

            _armyIdLabel.text = armySetup.ArmyId;
            _warriorsSlider.value = armySetup.WarriorsCount;
            _archerSlider.value = armySetup.ArchersCount;
            _armyColor.color = armySetup.ArmyColor;

            _strategyTypeWrapper = new EnumDropdownWrapper<StrategyType>(_strategyDropdown);
            _strategyDropdown.SetValueWithoutNotify((int)armySetup.StrategyType);
        }

        public ArmySetup CreateArmySetup()
        {
            int warriorsCount = (int)_warriorsSlider.value;
            int archersCount = (int)_archerSlider.value;
            StrategyType strategyType = _strategyTypeWrapper.Value();

            return new ArmySetup(_armyIdLabel.text, warriorsCount, archersCount, strategyType, _armyColor.color);
        }

        public void RefreshWarriorsCountLabel(float value)
        {
            _warriorsCountLabel.text = value.ToString();
        }

        public void RefreshArchersCountLabel(float value)
        {
            _archerCountLabel.text = value.ToString();
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

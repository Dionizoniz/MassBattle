using MassBattle.Core.Utilities;
using MassBattle.Logic.Setup;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MassBattle.UI.LaunchMenu
{
    public class ArmyPanelController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI armyIdLabel;

        [Space, SerializeField]
        private Slider warriorsSlider;
        [SerializeField]
        private TextMeshProUGUI warriorsCountLabel;

        [Space, SerializeField]
        private Slider archerSlider;
        [SerializeField]
        private TextMeshProUGUI archerCountLabel;

        [Space, SerializeField]
        private TMP_Dropdown strategyDropdown;
        [SerializeField]
        private Image armyColor;

        private EnumDropdownWrapper<ArmyStrategy> strategyTypeWrapper;

        public void InitializeData(ArmySetup armySetup)
        {
            armyIdLabel.text = armySetup.ArmyId;
            warriorsSlider.value = armySetup.WarriorsCount;
            archerSlider.value = armySetup.ArchersCount;

            strategyTypeWrapper = new EnumDropdownWrapper<ArmyStrategy>(strategyDropdown);
            strategyDropdown.SetValueWithoutNotify((int)armySetup.StrategyType);
        }

        public ArmySetup CreateArmySetup()
        {
            int warriorsCount = (int)warriorsSlider.value;
            int archersCount = (int)archerSlider.value;
            ArmyStrategy strategyType = strategyTypeWrapper.Value();

            return new ArmySetup(armyIdLabel.text, warriorsCount, archersCount, strategyType, armyColor.color);
        }

        public void RefreshWarriorsCountLabel(float value)
        {
            warriorsCountLabel.text = value.ToString();
        }

        public void RefreshArchersCountLabel(float value)
        {
            archerCountLabel.text = value.ToString();
        }

        private void OnDestroy()
        {
            strategyTypeWrapper.Dispose();
        }
    }
}

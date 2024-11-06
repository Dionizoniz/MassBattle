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
        [SerializeField]
        private Slider warriorsSlider;
        [SerializeField]
        private Slider archerSlider;
        [SerializeField]
        private TMP_Dropdown strategyDropdown;

        private int WarriorsCount => (int)warriorsSlider.value;
        private int ArchersCount => (int)archerSlider.value;

        private EnumDropdownWrapper<ArmyStrategy> strategyTypeWrapper;

        public void InitializeData(ArmySetup armySetup)
        {
            armyIdLabel.text = armySetup.Id;
            warriorsSlider.SetValueWithoutNotify(armySetup.WarriorsCount);
            archerSlider.SetValueWithoutNotify(armySetup.ArchersCount);

            strategyTypeWrapper = new EnumDropdownWrapper<ArmyStrategy>(strategyDropdown);
            strategyDropdown.SetValueWithoutNotify((int)armySetup.StrategyType);
        }

        public ArmySetup CreateArmySetup()
        {
            return new ArmySetup(armyIdLabel.text, WarriorsCount, ArchersCount, strategyTypeWrapper.Value());
        }
    }
}

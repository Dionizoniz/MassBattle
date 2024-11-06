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
        private TMP_Dropdown strategyDropdown;
        [SerializeField]
        private Slider warriorsSlider;
        [SerializeField]
        private Slider archerSlider;

        private EnumDropdownWrapper<ArmyStrategy> enumDropdown;

        private void InitializeData()
        {
            enumDropdown = new EnumDropdownWrapper<ArmyStrategy>(strategyDropdown);
        }

        private ArmySetup CreateArmySetup()
        {
            ArmyStrategy strategyType = enumDropdown.Value();

            return new ArmySetup(armyIdLabel.text, (int)warriorsSlider.value, (int)archerSlider.value, strategyType);
        }
    }
}

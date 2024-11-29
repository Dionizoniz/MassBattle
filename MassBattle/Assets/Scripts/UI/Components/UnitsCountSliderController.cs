using MassBattle.Core.Engine;
using MassBattle.Logic.Databases.Units;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MassBattle.UI.Components
{
    public class UnitsCountSliderController : ExtendedMonoBehaviour
    {
        [SerializeField]
        private Slider _slider;
        [SerializeField]
        private TextMeshProUGUI _unitsNameLabel;
        [SerializeField]
        private TextMeshProUGUI _countLabel;

        public string UnitId { get; private set; }
        public int UnitsCount => (int)_slider.value;

        public void Initialize(UnitDescriptor unitDescriptor, int initialCount, int minSliderValue, int maxSliderValue)
        {
            UnitId = unitDescriptor.DescriptorId;
            _unitsNameLabel.text = unitDescriptor.Name;

            _slider.minValue = minSliderValue;
            _slider.maxValue = maxSliderValue;

            _slider.SetValueWithoutNotify(initialCount);
            UpdateCountLabel(initialCount);
        }

        public void UpdateCountLabel(float value)
        {
            _countLabel.text = value.ToString();
        }
    }
}

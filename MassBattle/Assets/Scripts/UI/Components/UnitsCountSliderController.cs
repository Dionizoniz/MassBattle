using MassBattle.Core.Entities.Engine;
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
        private TextMeshProUGUI _countLabel;

        public string UnitId { get; private set; }
        public int UnitsCount => (int)_slider.value;

        public void Initialize(string unitId, int count)
        {
            UnitId = unitId;

            _slider.SetValueWithoutNotify(count);
            UpdateCountLabel(count);
        }

        public void UpdateCountLabel(float value)
        {
            _countLabel.text = value.ToString();
        }
    }
}

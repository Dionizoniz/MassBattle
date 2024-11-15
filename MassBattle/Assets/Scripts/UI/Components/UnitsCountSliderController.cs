using MassBattle.Core.Entities.Engine;
using MassBattle.Logic.Databases.UnitDatabase;
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

        public void Initialize(UnitData unitData, int initialCount)
        {
            UnitId = unitData.Id;
            _unitsNameLabel.text = unitData.UnitName;

            _slider.SetValueWithoutNotify(initialCount);
            UpdateCountLabel(initialCount);
        }

        public void UpdateCountLabel(float value)
        {
            _countLabel.text = value.ToString();
        }
    }
}

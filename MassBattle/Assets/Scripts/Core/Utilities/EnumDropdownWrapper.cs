using System;
using System.Collections.ObjectModel;
using System.Linq;
using TMPro;

namespace Core.Utilities
{
    public class EnumDropdownWrapper<T> : IDisposable where T : Enum
    {
        public event Action<T> OnValueChanged;

        private readonly TMP_Dropdown _dropdown;
        private readonly ReadOnlyDictionary<string, int> _nameToIndex;

        public EnumDropdownWrapper(TMP_Dropdown dropdown)
        {
            _dropdown = dropdown;

            _nameToIndex = new ReadOnlyDictionary<string, int>(Enum.GetNames(typeof(T))
                                                                   .Select((name, index) => (name, index))
                                                                   .ToDictionary(x => x.name, x => x.index));

            dropdown.ClearOptions();
            dropdown.AddOptions(_nameToIndex.Keys.ToList());
            dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }

        private void OnDropdownValueChanged(int index)
        {
            OnValueChanged?.Invoke((T)Enum.GetValues(typeof(T)).GetValue(index));
        }

        public T Value() => (T)Enum.GetValues(typeof(T)).GetValue(_dropdown.value);

        public void SetValueWithoutNotify(T value)
        {
            _dropdown.SetValueWithoutNotify(EnumToIndex(value));
        }

        private int EnumToIndex(T value) => _nameToIndex[value.ToString()];

        public void Dispose()
        {
            _dropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);
        }
    }
}

using MassBattle.Core.Descriptors;
using MassBattle.Core.Providers;
using UnityEngine;

namespace MassBattle.Logic.Databases.Colors
{
    [CreateAssetMenu(menuName = ConstantValues.DESCRIPTOR_MENU_GROUP + nameof(ColorDescriptor),
                     fileName = nameof(ColorDescriptor), order = ConstantValues.DESCRIPTOR_MENU_ORDER)]
    public class ColorDescriptor : Descriptor, IColorDescriptor
    {
        [SerializeField]
        private Color _color = Color.white;

        public Color Color => _color;

        public override bool IsSetupCorrect() => true;
    }
}

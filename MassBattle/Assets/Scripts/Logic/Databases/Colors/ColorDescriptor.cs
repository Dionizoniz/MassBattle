using MassBattle.Core.Descriptors;
using UnityEngine;

namespace MassBattle.Logic.Databases.Colors
{
    [CreateAssetMenu(menuName = "Descriptors/" + nameof(ColorDescriptor), fileName = nameof(ColorDescriptor),
                     order = -10000)]
    public class ColorDescriptor : Descriptor
    {
        [SerializeField]
        private Color _color = Color.white;

        public Color Color => _color;
    }
}

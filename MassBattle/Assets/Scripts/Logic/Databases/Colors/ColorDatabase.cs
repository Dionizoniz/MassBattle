using MassBattle.Core.Databases;
using UnityEngine;

namespace MassBattle.Logic.Databases.Colors
{
    [CreateAssetMenu(menuName = "Databases/" + nameof(ColorDatabase), fileName = nameof(ColorDatabase), order = -10000)]
    public class ColorDatabase : Database<ColorDescriptor>, IColorDatabase
    {
        [Space, SerializeField]
        private Color _damageColor = Color.red;
        [SerializeField]
        private Color _deathColor = Color.black;

        public Color DamageColor => _damageColor;
        public Color DeathColor => _deathColor;

        public int FindColorIndex(Color color)
        {
            return _elements.FindIndex(element => element.Color == color);
        }
    }
}

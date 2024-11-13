using MassBattle.Core.Entities.Database;
using UnityEngine;

namespace MassBattle.Logic.Databases
{
    [CreateAssetMenu(menuName = "Databases/" + nameof(ColorDatabase), fileName = nameof(ColorDatabase), order = 0)]
    public class ColorDatabase : Database<ColorData>, IColorDatabase
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

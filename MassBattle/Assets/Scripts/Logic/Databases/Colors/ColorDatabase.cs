using System.Collections.Generic;
using MassBattle.Core.Databases;
using MassBattle.Core.Providers;
using UnityEngine;

namespace MassBattle.Logic.Databases.Colors
{
    [CreateAssetMenu(menuName = ConstantValues.DATABASE_MENU_GROUP + nameof(ColorDatabase),
                     fileName = nameof(ColorDatabase), order = ConstantValues.DATABASE_MENU_ORDER)]
    public class ColorDatabase : Database<ColorDescriptor>, IColorDatabase
    {
        [Space, SerializeField]
        private Color _damageColor = Color.red;
        [SerializeField]
        private Color _deathColor = Color.black;

        public Color DamageColor => _damageColor;
        public Color DeathColor => _deathColor;

        public List<ColorDescriptor> AllColors => _descriptors;

        public int FindColorIndex(Color color)
        {
            return _descriptors.FindIndex(element => element.Color == color);
        }
    }
}

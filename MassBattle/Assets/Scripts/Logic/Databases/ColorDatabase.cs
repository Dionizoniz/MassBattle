using MassBattle.Core.Entities;
using UnityEngine;

namespace MassBattle.Logic.Databases
{
    [CreateAssetMenu(menuName = "Databases/" + nameof(ColorDatabase), fileName = nameof(ColorDatabase), order = 0)]
    public class ColorDatabase : BaseDatabase<ColorData>, IColorDatabase
    {
        public int FindColorIndex(Color color)
        {
            return _elements.FindIndex(element => element.Color == color);
        }
    }
}

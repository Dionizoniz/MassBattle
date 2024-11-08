using UnityEngine;

namespace MassBattle.Logic.Databases
{
    [CreateAssetMenu(menuName = "Databases/" + nameof(ColorDatabase), fileName = nameof(ColorDatabase), order = 0)]
    public class ColorDatabase : BaseDatabase<ColorData>
    { }
}

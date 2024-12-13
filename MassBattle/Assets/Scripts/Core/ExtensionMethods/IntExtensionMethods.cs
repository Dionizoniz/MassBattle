using UnityEngine;

namespace MassBattle.Core.ExtensionMethods
{
    public static class IntExtensionMethods
    {
        public static bool IsInRange(this int value, Vector2 range)
        {
            return value.IsInRange(range.x, range.y);
        }

        public static bool IsInRange(this int value, int min, int max)
        {
            return value.IsInRange((float)min, max);
        }

        public static bool IsInRange(this int value, float min, float max)
        {
            return value >= min && value <= max;
        }
    }
}

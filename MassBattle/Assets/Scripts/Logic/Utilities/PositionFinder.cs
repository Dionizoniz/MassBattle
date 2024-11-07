using System.Collections.Generic;
using MassBattle.Logic.Units;
using UnityEngine;

namespace MassBattle.Logic.Utilities
{
    public static class PositionFinder
    {
        public static Vector3 FindRandomPositionIn(Bounds bounds)
        {
            Vector3 position = Vector3.zero;

            position.x = Random.Range(bounds.min.x, bounds.max.x);
            position.z = Random.Range(bounds.min.z, bounds.max.z);

            return position;
        }

        public static Vector3 FindCenterOf<T>(List<T> elements) where T : Component
        {
            Vector3 result = Vector3.zero;

            foreach (var element in elements)
            {
                result += element.transform.position;
            }

            result /= elements.Count;

            return result;
        }

        public static float FindNearestUnit(BaseUnit source, List<BaseUnit> targets, out BaseUnit nearestUnits)
        {
            float minDistance = float.MaxValue;
            Vector3 sourcePosition = source.transform.position;
            nearestUnits = null;

            for (var index = 0; index < targets.Count; index++)
            {
                BaseUnit target = targets[index];
                float distance = Vector3.Distance(sourcePosition, target.transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestUnits = target;
                }
            }

            return minDistance;
        }
    }
}

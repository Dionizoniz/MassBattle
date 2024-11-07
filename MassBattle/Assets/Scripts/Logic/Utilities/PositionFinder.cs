using System.Collections.Generic;
using MassBattle.Logic.Units;
using UnityEngine;

namespace MassBattle.Logic.Utilities
{
    public static class PositionFinder
    {
        public static Vector3 FindRandomPositionIn(Bounds bounds)
        {
            Vector3 pos = Vector3.zero;
            pos.x = Random.Range(bounds.min.x, bounds.max.x);
            pos.z = Random.Range(bounds.min.z, bounds.max.z);
            return pos;
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
            nearestUnits = null;

            foreach (var target in targets)
            {
                float distance = Vector3.Distance(source.transform.position, target.transform.position);

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

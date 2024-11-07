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

        public static Vector3 FindCenterOf<T>(List<T> objects) where T : Component
        {
            Vector3 result = Vector3.zero;

            foreach (var o in objects)
            {
                result += o.transform.position;
            }

            result.x /= objects.Count;
            result.y /= objects.Count;
            result.z /= objects.Count;

            return result;
        }

        public static Vector3 FindCenterOf(List<BaseUnit> units)
        {
            Vector3 result = Vector3.zero;

            foreach (var unit in units)
            {
                result += unit.transform.position;
            }

            result.x /= units.Count;
            result.y /= units.Count;
            result.z /= units.Count;

            return result;
        }

        public static Vector3 FindCenterOf(List<GameObject> objects)
        {
            Vector3 result = Vector3.zero;

            foreach (var o in objects)
            {
                result += o.transform.position;
            }

            result.x /= objects.Count;
            result.y /= objects.Count;
            result.z /= objects.Count;

            return result;
        }

        public static float FindNearestObject(GameObject source, List<BaseUnit> objects, out BaseUnit nearestObject)
        {
            float minDist = float.MaxValue;
            nearestObject = null;

            foreach (var obj in objects)
            {
                float dist = Vector3.Distance(source.transform.position, obj.transform.position);

                if (dist < minDist)
                {
                    minDist = dist;
                    nearestObject = obj;
                }
            }

            return minDist;
        }
    }
}

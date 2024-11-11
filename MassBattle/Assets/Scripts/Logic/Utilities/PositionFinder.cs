using System.Collections.Generic;
using MassBattle.Logic.Armies;
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

        public static BaseUnit FindNearestUnit(BaseUnit source, ArmyData targetArmyData)
        {
            List<BaseUnit> targets = targetArmyData.FindAllUnits();
            Vector3 sourcePosition = source.transform.position;

            BaseUnit nearestUnits = null;
            float minDistance = float.MaxValue;

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

            return nearestUnits;
        }

        public static Vector3 FindCenterOfUnitsInRange(BaseUnit source, List<BaseUnit> units, float range)
        {
            Vector3 sourcePosition = source.transform.position;
            float squareRange = range * range;
            Vector3 centerOfUnits = Vector3.zero;
            int unitsInRange = 0;

            for (int i = 0; i < units.Count; i++)
            {
                Vector3 unitPosition = units[i].transform.position;
                float squareMagnitude = (sourcePosition - unitPosition).sqrMagnitude;

                if (squareMagnitude <= squareRange)
                {
                    centerOfUnits += unitPosition;
                    unitsInRange++;
                }
            }

            return centerOfUnits / unitsInRange;
        }
    }
}

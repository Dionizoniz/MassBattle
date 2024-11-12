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

        public static BaseUnit FindNearestUnit(BaseUnit source, List<ArmyData> targetArmyData)
        {
            BaseUnit nearestUnits = null;
            float minMagnitude = float.MaxValue;

            foreach (ArmyData armyData in targetArmyData)
            {
                BaseUnit unit = FindNearestUnit(source, armyData, out var magnitude);

                if (magnitude < minMagnitude)
                {
                    nearestUnits = unit;
                }
            }

            return nearestUnits;
        }

        private static BaseUnit FindNearestUnit(BaseUnit source, ArmyData targetArmyData, out float distance)
        {
            Vector3 sourcePosition = source.cachedPosition;
            List<BaseUnit> targetUnits = targetArmyData.AllUnits;

            BaseUnit nearestUnits = null;
            float minSquareMagnitude = float.MaxValue;

            for (var index = 0; index < targetUnits.Count; index++)
            {
                BaseUnit target = targetUnits[index];
                float squareMagnitude = (sourcePosition - target.cachedPosition).sqrMagnitude;

                if (squareMagnitude < minSquareMagnitude)
                {
                    minSquareMagnitude = squareMagnitude;
                    nearestUnits = target;
                }
            }

            distance = Mathf.Sqrt(minSquareMagnitude);

            return nearestUnits;
        }

        public static Vector3 FindCenterOf(List<BaseUnit> units)
        {
            Vector3 center = Vector3.zero;

            for (int i = 0; i < units.Count; i++)
            {
                center += units[i].cachedPosition;
            }

            return center / units.Count;
        }

        public static Vector3 FindCenterOfUnitsInRange(BaseUnit source, List<BaseUnit> units, float range)
        {
            Vector3 sourcePosition = source.cachedPosition;
            float squareRange = range * range;
            Vector3 centerOfUnits = Vector3.zero;
            int unitsInRange = 0;

            for (int i = 0; i < units.Count; i++)
            {
                Vector3 unitPosition = units[i].cachedPosition;
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

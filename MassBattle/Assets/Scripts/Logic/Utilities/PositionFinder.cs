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

        public static BaseUnit FindNearestUnit(BaseUnit source, ArmyData targetArmyData)
        {
            List<BaseUnit> targets = targetArmyData.AllUnits;
            Vector3 sourcePosition = source.cachedPosition;

            BaseUnit nearestUnits = null;
            float minSquareMagnitude = float.MaxValue;

            for (var index = 0; index < targets.Count; index++)
            {
                BaseUnit target = targets[index];
                float squareMagnitude = (sourcePosition - target.cachedPosition).sqrMagnitude;

                if (squareMagnitude < minSquareMagnitude)
                {
                    minSquareMagnitude = squareMagnitude;
                    nearestUnits = target;
                }
            }

            return nearestUnits;
        }

        // TODO TEMPORARY CODE
        public static BaseUnit FindNearestUnit(BaseUnit source, List<ArmyData> targetArmyData)
        {
            Vector3 sourcePosition = source.cachedPosition;
            BaseUnit nearestUnits = null;
            float minSquareMagnitude = float.MaxValue;

            for (int i = 0; i < targetArmyData.Count; i++)
            {
                List<BaseUnit> targets = targetArmyData[i].AllUnits;

                for (var index = 0; index < targets.Count; index++)
                {
                    BaseUnit target = targets[index];
                    float squareMagnitude = (sourcePosition - target.cachedPosition).sqrMagnitude;

                    if (squareMagnitude < minSquareMagnitude)
                    {
                        minSquareMagnitude = squareMagnitude;
                        nearestUnits = target;
                    }
                }
            }

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

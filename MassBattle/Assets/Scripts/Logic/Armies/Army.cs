using System.Collections.Generic;
using System.Linq;
using MassBattle.Logic.Units;
using UnityEngine;

namespace MassBattle.Logic.Armies
{
    public class Army
    {
        public List<Warrior> warriors = new();
        public List<Archer> archers = new();

        public Army enemyArmy;
        public Color color;

        public int CalculateUnitsCount() => warriors.Count + archers.Count;

        public List<BaseUnit> FindAllUnits() // TODO Optimize
        {
            List<BaseUnit> units = new();
            units.AddRange(warriors);
            units.AddRange(archers);

            return units;
        }
    }
}

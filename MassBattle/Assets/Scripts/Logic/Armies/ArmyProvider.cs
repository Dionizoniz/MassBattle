using System.Collections.Generic;
using System.Linq;

namespace MassBattle.Logic.Armies
{
    public class ArmyProvider : IArmyProvider
    {
        private List<Army> armies = new();

        public void ClearArmies()
        {
            armies.Clear();
        }

        public void RegisterArmy(Army army)
        {
            if (armies.Contains(army))
            {
                armies.Remove(army);
            }

            armies.Add(army);
        }

        public Army FindArmyBy(string armyId)
        {
            return armies.FirstOrDefault(army => army.id == armyId);
        }

        public void FillUpEnemiesForRegisteredArmies() // TODO simplify logic ???
        {
            for (int armyIndex = 0; armyIndex < armies.Count; armyIndex++)
            {
                for (int enemyIndex = 0; enemyIndex < armies.Count; enemyIndex++)
                {
                    if (armyIndex != enemyIndex) // TODO improve logic and make it as a list
                    {
                        armies[armyIndex].enemyArmy = armies[enemyIndex];
                    }
                }
            }
        }
    }
}

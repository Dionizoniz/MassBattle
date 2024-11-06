using System.Collections.Generic;
using System.Linq;

namespace MassBattle.Logic.Armies
{
    public class ArmyProvider : IArmyProvider
    {
        private List<ArmyData> armies = new(); // TODO new name???

        public void ClearArmies()
        {
            armies.Clear();
        }

        public void RegisterArmy(ArmyData armyData)
        {
            if (armies.Contains(armyData))
            {
                armies.Remove(armyData);
            }

            armies.Add(armyData);
        }

        public ArmyData FindArmyBy(string armyId)
        {
            return armies.FirstOrDefault(army => army.ArmySetup.ArmyId == armyId);
        }

        public void FillUpEnemiesForRegisteredArmies() // TODO simplify logic ???
        {
            for (int armyIndex = 0; armyIndex < armies.Count; armyIndex++)
            {
                for (int enemyIndex = 0; enemyIndex < armies.Count; enemyIndex++)
                {
                    if (armyIndex != enemyIndex) // TODO improve logic and make it as a list
                    {
                        armies[armyIndex].enemyArmyData = armies[enemyIndex];
                    }
                }
            }
        }
    }
}

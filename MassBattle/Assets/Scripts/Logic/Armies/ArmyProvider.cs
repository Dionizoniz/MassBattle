using System.Collections.Generic;
using System.Linq;

namespace MassBattle.Logic.Armies
{
    public class ArmyProvider : IArmyProvider
    {
        private readonly List<ArmyData> _armiesData = new();

        public void ClearArmies()
        {
            _armiesData.Clear();
        }

        public void RegisterArmy(ArmyData armyData)
        {
            if (_armiesData.Contains(armyData))
            {
                _armiesData.Remove(armyData);
            }

            _armiesData.Add(armyData);
        }

        public ArmyData FindArmyBy(string armyId)
        {
            return _armiesData.FirstOrDefault(army => army.ArmySetup.ArmyId == armyId);
        }

        public void FillUpEnemiesForRegisteredArmies() // TODO simplify logic ???
        {
            for (int armyIndex = 0; armyIndex < _armiesData.Count; armyIndex++)
            {
                for (int enemyIndex = 0; enemyIndex < _armiesData.Count; enemyIndex++)
                {
                    if (armyIndex != enemyIndex) // TODO improve logic and make it as a list
                    {
                        _armiesData[armyIndex].enemyArmyData = _armiesData[enemyIndex];
                    }
                }
            }
        }
    }
}

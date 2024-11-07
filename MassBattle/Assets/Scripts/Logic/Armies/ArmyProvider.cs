using System.Collections.Generic;
using System.Linq;

namespace MassBattle.Logic.Armies
{
    public class ArmyProvider : IArmyProvider
    {
        private readonly List<ArmyData> _armies = new(); // TODO new name???

        public void ClearArmies()
        {
            _armies.Clear();
        }

        public void RegisterArmy(ArmyData armyData)
        {
            if (_armies.Contains(armyData))
            {
                _armies.Remove(armyData);
            }

            _armies.Add(armyData);
        }

        public ArmyData FindArmyBy(string armyId)
        {
            return _armies.FirstOrDefault(army => army.ArmySetup.ArmyId == armyId);
        }

        public void FillUpEnemiesForRegisteredArmies() // TODO simplify logic ???
        {
            for (int armyIndex = 0; armyIndex < _armies.Count; armyIndex++)
            {
                for (int enemyIndex = 0; enemyIndex < _armies.Count; enemyIndex++)
                {
                    if (armyIndex != enemyIndex) // TODO improve logic and make it as a list
                    {
                        _armies[armyIndex].enemyArmyData = _armies[enemyIndex];
                    }
                }
            }
        }
    }
}

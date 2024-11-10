using MassBattle.Logic.Setup;
using MassBattle.Logic.Strategies;
using MassBattle.Logic.Weapons;
using UnityEngine;

namespace MassBattle.Logic.Units
{
    public class Archer : BaseUnit
    {
        [Space, SerializeField]
        private Arrow _arrowPrefab;

        protected override IStrategy CreateStrategy(StrategyType strategyType)
        {
            return strategyType switch
            {
                    StrategyType.Basic => new SimpleArcherStrategy(),
                    StrategyType.Defensive => new DefenceArcherStrategy(),
                    _ => FindDefaultStrategy()
            };

            IStrategy FindDefaultStrategy()
            {
                Debug.LogError("Create default strategy instead for missing StrategyType in Archer.cs");
                return new SimpleArcherStrategy();
            }
        }

        protected override void PerformAttack(BaseUnit enemy)
        {
            Arrow spawnedArrow = Instantiate(_arrowPrefab);
            spawnedArrow.Initialize(this, enemy, ArmyData.ArmySetup.ArmyColor);
        }
    }
}

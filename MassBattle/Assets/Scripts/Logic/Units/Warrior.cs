using MassBattle.Logic.Setup;
using MassBattle.Logic.Strategies;
using UnityEngine;

namespace MassBattle.Logic.Units
{
    public class Warrior : BaseUnit
    {
        protected override IStrategy CreateStrategy(StrategyType strategyType)
        {
            return strategyType switch
            {
                    StrategyType.Basic => new SimpleStrategy(),
                    StrategyType.Defensive => new DefenceWarriorStrategy(),
                    _ => FindDefaultStrategy()
            };

            IStrategy FindDefaultStrategy()
            {
                Debug.LogError("Create default strategy instead for missing StrategyType in Warrior.cs");
                return new SimpleStrategy();
            }
        }

        protected override void PerformAttack(BaseUnit enemy)
        {
            enemy.Hit(gameObject);
        }
    }
}

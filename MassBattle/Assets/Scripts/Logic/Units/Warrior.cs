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
                    StrategyType.Basic => new SimpleStrategy(this),
                    StrategyType.Defensive => new DefenceWarriorStrategy(this),
                    _ => FindDefaultStrategy()
            };

            IStrategy FindDefaultStrategy()
            {
                Debug.LogError("Create default strategy instead for missing StrategyType in Warrior.cs");
                return new SimpleStrategy(this);
            }
        }

        protected override void PerformAttack(BaseUnit enemy)
        {
            enemy.Hit(gameObject);
        }
    }
}

using MassBattle.Logic.Strategies;
using MassBattle.Logic.Units.Weapons;
using UnityEngine;
using Zenject;

namespace MassBattle.Logic.Units
{
    public class Archer : BaseUnit
    {
        [Space, SerializeField]
        private Arrow _arrowPrefab;

        private DiContainer _container;

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
        }

        protected override IStrategy CreateStrategy(StrategyType strategyType)
        {
            return strategyType switch
            {
                    StrategyType.Basic => new SimpleStrategy(this),
                    StrategyType.Defensive => new DefenceArcherStrategy(this),
                    _ => FindDefaultStrategy()
            };

            IStrategy FindDefaultStrategy()
            {
                Debug.LogError("Create default strategy instead for missing StrategyType in Archer.cs");
                return new SimpleStrategy(this);
            }
        }

        protected override void PerformAttack(BaseUnit enemy)
        {
            Arrow spawnedArrow = _unitsFactory.CreateArrowInstance(_arrowPrefab);
            _container.InjectGameObject(spawnedArrow._gameObject);
            spawnedArrow.Initialize(this, enemy, ArmyData.InitialArmyData.ArmyColor);
        }
    }
}

using System;
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

        protected override IStrategy CreateStrategy()
        {
            return ArmyData.ArmySetup.StrategyType switch
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

        protected override Vector3 UpdateDefensive(BaseUnit enemy)
        {
            Vector3 moveDirection;
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < _attackRange)
            {
                Vector3 toNearest = (enemy.transform.position - transform.position).normalized;
                toNearest.Scale(new Vector3(1, 0, 1));

                Vector3 flank = Quaternion.Euler(0, 90, 0) * toNearest;
                moveDirection = -(toNearest + flank).normalized;
            }
            else
            {
                Vector3 toNearest = (enemy.transform.position - transform.position).normalized;
                toNearest.Scale(new Vector3(1, 0, 1));
                moveDirection = toNearest.normalized;
            }

            return moveDirection;
        }

        protected override Vector3 UpdateBasic(BaseUnit enemy)
        {
            Vector3 toNearest = (enemy.transform.position - transform.position).normalized;
            toNearest.Scale(new Vector3(1, 0, 1));
            Vector3 moveDirection = toNearest.normalized;

            return moveDirection;
        }
    }
}

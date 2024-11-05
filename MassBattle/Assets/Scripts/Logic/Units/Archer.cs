using System.Collections.Generic;
using UnityEngine;

namespace MassBattle.Logic.Units
{
    public class Archer : BaseUnit
    {
        [Space, SerializeField]
        private ArcherArrow arrowPrefab;

        public override void Attack(BaseUnit enemy)
        {
            if (attackCooldown > 0)
                return;

            if (Vector3.Distance(transform.position, enemy.transform.position) > attackRange)
                return;

            Color arrowColor = army == battleInstaller.ArmyProvider.Army1 ?
                                       battleInstaller.ArmyProvider.Army1.color :
                                       battleInstaller.ArmyProvider.Army2.color;

            attackCooldown = maxAttackCooldown;
            ArcherArrow spawnedArrow = Instantiate(arrowPrefab);
            spawnedArrow.Initialize(this, enemy.GetComponent<BaseUnit>(), arrowColor);

            animator.SetTrigger("Attack");
        }

        public void OnDeathAnimFinished()
        {
            Destroy(gameObject);
        }

        protected override void UpdateDefensive(List<BaseUnit> allies, List<BaseUnit> enemies)
        {
            Vector3 enemyCenter = Utils.GetCenter(enemies);
            float distToEnemyX = Mathf.Abs(enemyCenter.x - transform.position.x);

            if (distToEnemyX > attackRange)
            {
                if (enemyCenter.x < transform.position.x)
                    Move(Vector3.left);

                if (enemyCenter.x > transform.position.x)
                    Move(Vector3.right);
            }

            float distToNearest = Utils.GetNearestObject(gameObject, enemies, out BaseUnit nearestEnemy);

            if (nearestEnemy == null)
                return;

            if (distToNearest < attackRange)
            {
                Vector3 toNearest = (nearestEnemy.transform.position - transform.position).normalized;
                toNearest.Scale(new Vector3(1, 0, 1));

                Vector3 flank = Quaternion.Euler(0, 90, 0) * toNearest;
                Move(-(toNearest + flank).normalized);
            }
            else
            {
                Vector3 toNearest = (nearestEnemy.transform.position - transform.position).normalized;
                toNearest.Scale(new Vector3(1, 0, 1));
                Move(toNearest.normalized);
            }

            Attack(nearestEnemy);
        }

        protected override void UpdateBasic(List<BaseUnit> allies, List<BaseUnit> enemies)
        {
            Utils.GetNearestObject(gameObject, enemies, out BaseUnit nearestEnemy);

            if (nearestEnemy == null)
                return;

            Vector3 toNearest = (nearestEnemy.transform.position - transform.position).normalized;
            toNearest.Scale(new Vector3(1, 0, 1));
            Move(toNearest.normalized);

            Attack(nearestEnemy);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace MassBattle.Logic.Units
{
    public class Archer : BaseUnit
    {
        [Space, SerializeField]
        private ArcherArrow arrowPrefab;

        public override void Attack(GameObject enemy)
        {
            if (attackCooldown > 0)
                return;

            if (Vector3.Distance(transform.position, enemy.transform.position) > attackRange)
                return;

            Color arrowColor = army == BattleInstantiator.instance.army1 ?
                                       BattleInstantiator.instance.army1Color :
                                       BattleInstantiator.instance.army2Color;

            attackCooldown = maxAttackCooldown;
            ArcherArrow spawnedArrow = Instantiate(arrowPrefab);
            spawnedArrow.Initialize(this, enemy.GetComponent<BaseUnit>(), arrowColor);

            animator.SetTrigger("Attack");
        }

        public void OnDeathAnimFinished()
        {
            Destroy(gameObject);
        }

        protected override void UpdateDefensive(List<GameObject> allies, List<GameObject> enemies)
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

            float distToNearest = Utils.GetNearestObject(gameObject, enemies, out GameObject nearestEnemy);

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

        protected override void UpdateBasic(List<GameObject> allies, List<GameObject> enemies)
        {
            Utils.GetNearestObject(gameObject, enemies, out GameObject nearestEnemy);

            if (nearestEnemy == null)
                return;

            Vector3 toNearest = (nearestEnemy.transform.position - transform.position).normalized;
            toNearest.Scale(new Vector3(1, 0, 1));
            Move(toNearest.normalized);

            Attack(nearestEnemy);
        }
    }
}

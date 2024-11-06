using System;
using System.Collections.Generic;
using System.Linq;
using MassBattle.Logic.Armies;
using MassBattle.Logic.Installers;
using UnityEngine;

namespace MassBattle.Logic.Units
{
    public abstract class BaseUnit : MonoBehaviour, IInitialize
    {
        [SerializeField]
        protected float health = 50f;
        [SerializeField]
        protected float speed = 0.1f;
        [SerializeField]
        protected float defense;

        [Space, SerializeField]
        protected float attack = 20f;
        [SerializeField]
        protected float attackRange = 2.5f;
        [SerializeField]
        protected float maxAttackCooldown = 1f;
        [SerializeField]
        protected float postAttackDelay;

        [Space, SerializeField]
        protected Animator animator;
        [SerializeField]
        private Renderer renderer;

        public float AttackValue => attack;

        public Army army;

        [NonSerialized]
        public IArmyModel armyModel;

        protected float attackCooldown;
        private Vector3 lastPosition;

        protected IBattleInstaller battleInstaller;

        protected Color color;

        public abstract void Attack(BaseUnit enemy);

        protected abstract void UpdateDefensive(List<BaseUnit> allies, List<BaseUnit> enemies);
        protected abstract void UpdateBasic(List<BaseUnit> allies, List<BaseUnit> enemies);

        public void Initialize(IBattleInstaller battleInstaller)
        {
            this.battleInstaller = battleInstaller;
        }

        public void SetColor(Color color)
        {
            this.color = color;

            MaterialPropertyBlock propertyBlock = new();
            propertyBlock.SetColor("_Color", color);
            renderer.SetPropertyBlock(propertyBlock);
        }

        public virtual void Move(Vector3 delta)
        {
            if (attackCooldown > maxAttackCooldown - postAttackDelay)
                return;

            transform.position += delta * speed;
        }

        public virtual void Hit(GameObject sourceGo)
        {
            BaseUnit source = sourceGo.GetComponent<BaseUnit>();
            float sourceAttack = 0;

            if (source != null)
            {
                sourceAttack = source.attack;
            }
            else
            {
                ArcherArrow arrow = sourceGo.GetComponent<ArcherArrow>();
                sourceAttack = arrow.attack;
            }

            health -= Mathf.Max(sourceAttack - defense, 0);

            if (health < 0)
            {
                transform.forward = sourceGo.transform.position - transform.position;

                if (this is Warrior)
                    army.warriors.Remove(this as Warrior);
                else if (this is Archer)
                    army.archers.Remove(this as Archer);

                animator.SetTrigger("Death");
            }
            else
            {
                animator.SetTrigger("Hit");
            }
        }

        private void Update()
        {
            if (health < 0)
                return;

            List<BaseUnit> allies = army.FindAllUnits();
            List<BaseUnit> enemies = army.enemyArmy.FindAllUnits();

            UpdateBasicRules(allies, enemies);

            switch (armyModel.strategy)
            {
                case ArmyStrategy.Defensive:
                    UpdateDefensive(allies, enemies);
                    break;
                case ArmyStrategy.Basic:
                    UpdateBasic(allies, enemies);
                    break;
            }

            animator.SetFloat("MovementSpeed", (transform.position - lastPosition).magnitude / speed);
            lastPosition = transform.position;
        }

        void UpdateBasicRules(List<BaseUnit> allies, List<BaseUnit> enemies)
        {
            attackCooldown -= Time.deltaTime;
            EvadeAllies(allies);
        }

        void EvadeAllies(List<BaseUnit> allies)
        {
            var allUnits = army.FindAllUnits().Union(army.enemyArmy.FindAllUnits()).ToList();

            Vector3 center = Utils.GetCenter(allUnits);

            float centerDist = Vector3.Distance(gameObject.transform.position, center);

            if (centerDist > 80.0f)
            {
                Vector3 toNearest = (center - transform.position).normalized;
                transform.position -= toNearest * (80.0f - centerDist);
                return;
            }

            foreach (var obj in allUnits)
            {
                float dist = Vector3.Distance(gameObject.transform.position, obj.transform.position);

                if (dist < 2f)
                {
                    Vector3 toNearest = (obj.transform.position - transform.position).normalized;
                    transform.position -= toNearest * (2.0f - dist);
                }
            }
        }
    }
}

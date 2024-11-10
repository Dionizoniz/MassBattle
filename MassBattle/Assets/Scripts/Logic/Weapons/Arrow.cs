using System;
using MassBattle.Logic.Armies;
using MassBattle.Logic.Units;
using UnityEngine;

namespace MassBattle.Logic.Weapons
{
    public class Arrow : MonoBehaviour // TODO refactor
    {
        public float speed;

        
        [NonSerialized]
        public float attack;

        private ArmyData _armyData;
        private Vector3 _target;

        public void Initialize(BaseUnit sourceUnit, BaseUnit targetUnit, Color color)
        {
            _target = targetUnit.transform.position;
            attack = sourceUnit.AttackValue;
            _armyData = sourceUnit.ArmyData;
            transform.position = sourceUnit.transform.position;

            GetComponent<Renderer>().material.color = color;
        }

        public void Update()
        {
            Vector3 direction = (_target - transform.position).normalized;
            transform.position += direction * speed;
            transform.forward = direction;

            foreach (var unit in _armyData.enemyArmyData.FindAllUnits())
            {
                float dist = Vector3.Distance(unit.transform.position, transform.position);

                if (dist < speed)
                {
                    unit.Hit(gameObject);
                    Destroy(gameObject);
                    return;
                }
            }

            if (Vector3.Distance(transform.position, _target) < speed)
            {
                Destroy(gameObject);
            }
        }
    }
}

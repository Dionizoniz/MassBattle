using System;
using MassBattle.Logic.Armies;
using MassBattle.Logic.Units;
using UnityEngine;

public class ArcherArrow : MonoBehaviour // TODO refactor
{
    public float speed;

    [NonSerialized]
    public Vector3 target;
    [NonSerialized]
    public float attack;

    public ArmyData armyData;

    public void Initialize(BaseUnit sourceUnit, BaseUnit targetUnit, Color color)
    {
        target = targetUnit.transform.position;
        attack = sourceUnit.AttackValue;
        armyData = sourceUnit.ArmyData;
        transform.position = sourceUnit.transform.position;

        GetComponent<Renderer>().material.color = color;
    }

    public void Update()
    {
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * speed;
        transform.forward = direction;

        foreach (var unit in armyData.enemyArmyData.FindAllUnits())
        {
            float dist = Vector3.Distance(unit.transform.position, transform.position);

            if (dist < speed)
            {
                unit.Hit(gameObject);
                Destroy(gameObject);
                return;
            }
        }

        if (Vector3.Distance(transform.position, target) < speed)
        {
            Destroy(gameObject);
        }
    }
}

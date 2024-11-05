using System.Collections.Generic;
using MassBattle.Logic.Units;
using UnityEngine;

public class Army
{
    public Army enemyArmy;
    public Color color;
    public List<Warrior> warriors = new List<Warrior>();
    public List<Archer> archers = new List<Archer>();

    public List<GameObject> GetUnits()
    {
        List<GameObject> result = new List<GameObject>();

        foreach (var warrior in warriors)
        {
            result.Add(warrior.gameObject);
        }

        foreach (var archer in archers)
        {
            result.Add(archer.gameObject);
        }

        return result;
    }
}

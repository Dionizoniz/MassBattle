using MassBattle.Logic.Installers;
using MassBattle.Logic.Units;
using UnityEngine;

namespace MassBattle.Logic.BattleCreator
{
    public class BattleSpawner : MonoBehaviour, IBattleSpawner, IInitialize
    {
        [SerializeField]
        private ArmyModelSO army1Model;

        [SerializeField]
        private ArmyModelSO army2Model;

        [SerializeField]
        private Warrior warriorPrefab;

        [SerializeField]
        private Archer archerPrefab;

        [SerializeField]
        private BoxCollider leftArmySpawnBounds;

        [SerializeField]
        private BoxCollider rightArmySpawnBounds;

        private IBattleInstaller battleInstaller;

        public readonly Army army1 = new(); // TODO improve
        public readonly Army army2 = new(); // TODO improve

        public Color army1Color; // TODO improve
        public Color army2Color; // TODO improve

        public void Initialize(IBattleInstaller battleInstaller)
        {
            this.battleInstaller = battleInstaller;

            army1.color = army1Color;
            army1.enemyArmy = army2;

            army2.color = army2Color;
            army2.enemyArmy = army1;

            SpawnArmy(army1Model, army1, leftArmySpawnBounds.bounds);
            SpawnArmy(army2Model, army2, rightArmySpawnBounds.bounds);
        }

        private void SpawnArmy(IArmyModel model, Army army, Bounds instanceBounds)
        {
            for (int i = 0; i < model.warriors; i++)
            {
                Warrior spawnedUnit = SpawnUnit(warriorPrefab, model, army, instanceBounds);
                army.warriors.Add(spawnedUnit);
            }

            for (int i = 0; i < model.archers; i++)
            {
                Archer spawnedUnit = SpawnUnit(archerPrefab, model, army, instanceBounds);
                army.archers.Add(spawnedUnit);
            }
        }

        private T SpawnUnit<T>(T unitToSpawn, IArmyModel model, Army army, Bounds instanceBounds) where T : BaseUnit
        {
            T spawnedUnit = Instantiate(unitToSpawn);

            spawnedUnit.transform.position = Utils.GetRandomPosInBounds(instanceBounds);

            spawnedUnit.army = army;
            spawnedUnit.armyModel = model;
            spawnedUnit.GetComponentInChildren<Renderer>().material.color = army.color;

            return spawnedUnit;
        }
    }
}

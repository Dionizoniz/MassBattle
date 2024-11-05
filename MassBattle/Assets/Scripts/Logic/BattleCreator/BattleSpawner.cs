using MassBattle.Logic.Installers;
using MassBattle.Logic.Units;
using UnityEngine;

namespace MassBattle.Logic.BattleCreator
{
    public class BattleSpawner : MonoBehaviour, IBattleSpawner
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

        private readonly Army army1 = new(); // TODO improve
        private readonly Army army2 = new(); // TODO improve

        public Color army1Color; // TODO improve
        public Color army2Color; // TODO improve

        public Army Army1
        {
            get => army1;
        }
        public Army Army2
        {
            get => army2;
        }

        public void Initialize(IBattleInstaller battleInstaller)
        {
            this.battleInstaller = battleInstaller;
        }

        private void Start()
        {
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
            spawnedUnit.Initialize(battleInstaller);

            spawnedUnit.transform.position = Utils.GetRandomPosInBounds(instanceBounds);

            spawnedUnit.army = army;
            spawnedUnit.armyModel = model;
            spawnedUnit.GetComponentInChildren<Renderer>().material.color = army.color;

            return spawnedUnit;
        }

        private Vector3 forwardTarget; // TODO improve solution - now is moved only

        void Update()
        {
            if (army1.GetUnits().Count == 0 || army2.GetUnits().Count == 0)
            {
                // TODO improve logic
                // gameOverMenu.gameObject.SetActive(true); 
                // gameOverMenu.Populate();
            }

            Vector3 mainCenter = Utils.GetCenter(army1.GetUnits()) + Utils.GetCenter(army2.GetUnits());
            mainCenter *= 0.5f;

            forwardTarget = (mainCenter - Camera.main.transform.position).normalized;

            Camera.main.transform.forward += (forwardTarget - Camera.main.transform.forward) * 0.1f;
        }
    }
}

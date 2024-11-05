using MassBattle.Logic.Armies;
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

        private IArmyProvider ArmyProvider => battleInstaller.ArmyProvider;

        private IBattleInstaller battleInstaller;

        public Color army1Color; // TODO improve
        public Color army2Color; // TODO improve

        public void Initialize(IBattleInstaller battleInstaller)
        {
            this.battleInstaller = battleInstaller;

            SpawnArmies();
        }

        private void SpawnArmies()
        {
            ArmyProvider.Army1.color = army1Color;
            ArmyProvider.Army1.enemyArmy = ArmyProvider.Army2;

            ArmyProvider.Army2.color = army2Color;
            ArmyProvider.Army2.enemyArmy = ArmyProvider.Army1;

            SpawnArmy(army1Model, ArmyProvider.Army1, leftArmySpawnBounds.bounds);
            SpawnArmy(army2Model, ArmyProvider.Army2, rightArmySpawnBounds.bounds);
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
            if (ArmyProvider.Army1.CalculateUnitsCount() == 0 || ArmyProvider.Army2.CalculateUnitsCount() == 0)
            {
                // TODO improve logic
                // gameOverMenu.gameObject.SetActive(true); 
                // gameOverMenu.Populate();
            }

            Vector3 mainCenter = Utils.GetCenter(ArmyProvider.Army1.FindAllUnits()) +
                                 Utils.GetCenter(ArmyProvider.Army2.FindAllUnits());

            mainCenter *= 0.5f;

            forwardTarget = (mainCenter - Camera.main.transform.position).normalized;

            Camera.main.transform.forward += (forwardTarget - Camera.main.transform.forward) * 0.1f;
        }
    }
}

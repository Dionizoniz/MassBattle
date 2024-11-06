using System.Collections.Generic;
using MassBattle.Logic.Armies;
using MassBattle.Logic.Installers;
using MassBattle.Logic.Setup;
using MassBattle.Logic.Units;
using UnityEngine;

namespace MassBattle.Logic.BattleCreator
{
    public class BattleSpawner : MonoBehaviour, IBattleSpawner
    {
        [SerializeField]
        private Warrior warriorPrefab;
        [SerializeField]
        private Archer archerPrefab;

        [Space, SerializeField]
        private List<BoxCollider> spawnArmyBounds = new(); // TODO check that size is same as colors
        [SerializeField]
        private List<Color> armyColors = new(); // TODO move colors to setup ???

        private IArmyProvider ArmyProvider => battleInstaller.ArmyProvider;

        private IBattleInstaller battleInstaller;

        public void Initialize(IBattleInstaller battleInstaller)
        {
            this.battleInstaller = battleInstaller;

            SpawnArmies();
        }

        private void SpawnArmies()
        {
            List<string> armyIds = battleInstaller.BattleSetup.FindAllArmySetupIds();

            ArmyProvider.ClearArmies();

            for (int i = 0; i < armyIds.Count; i++)
            {
                // TODO add index protections
                Army army = SpawnArmy(armyIds[i], armyColors[i], spawnArmyBounds[i].bounds);
                ArmyProvider.RegisterArmy(army);
            }

            ArmyProvider.FillUpEnemiesForRegisteredArmies();
        }

        private Army SpawnArmy(string armyId, Color color, Bounds spawnBounds) // TODO simplify code
        {
            ArmySetup armySetup = battleInstaller.BattleSetup.TryFindArmySetupBy(armyId);
            List<Warrior> warriors = new();
            List<Archer> archers = new();
            ArmyStrategy armyStrategy = ArmyStrategy.Basic; // improve

            if (armySetup != null)
            {
                for (int i = 0; i < armySetup.WarriorsCount; i++)
                {
                    Warrior spawnedWarrior = SpawnUnit(warriorPrefab, armyId, color, spawnBounds);
                    warriors.Add(spawnedWarrior);
                }

                for (int i = 0; i < armySetup.WarriorsCount; i++)
                {
                    Archer spawnedArcher = SpawnUnit(archerPrefab, armyId, color, spawnBounds);
                    archers.Add(spawnedArcher);
                }

                armyStrategy = armySetup.StrategyType;
            }
            else
            {
                Debug.LogError("Army Setup could not be found. Can not spawn army.");
            }

            return new Army(armyId, warriors, archers, armyStrategy, color);
        }

        private T SpawnUnit<T>(T unitToSpawn, string armyId, Color color, Bounds spawnBounds) where T : BaseUnit
        {
            T spawnedUnit = Instantiate(unitToSpawn);
            spawnedUnit.Initialize(battleInstaller);

            spawnedUnit.transform.position = Utils.GetRandomPosInBounds(spawnBounds);
            spawnedUnit.armyId = armyId;
            spawnedUnit.SetColor(color);

            return spawnedUnit;
        }

        private Vector3 forwardTarget; // TODO improve solution - now is moved only

        void Update()
        {
            //if (ArmyProvider.Army1.CalculateUnitsCount() == 0 || ArmyProvider.Army2.CalculateUnitsCount() == 0)
            {
                // TODO improve logic
                // gameOverMenu.gameObject.SetActive(true); 
                // gameOverMenu.Populate();
            }

            // Vector3 mainCenter = Utils.GetCenter(ArmyProvider.Army1.FindAllUnits()) +
            //                      Utils.GetCenter(ArmyProvider.Army2.FindAllUnits());
            //
            // mainCenter *= 0.5f;
            //
            // forwardTarget = (mainCenter - Camera.main.transform.position).normalized;
            //
            // Camera.main.transform.forward += (forwardTarget - Camera.main.transform.forward) * 0.1f;
        }
    }
}

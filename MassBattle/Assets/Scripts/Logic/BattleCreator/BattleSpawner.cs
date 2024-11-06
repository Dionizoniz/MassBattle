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
                ArmyData armyData = SpawnArmy(armyIds[i], spawnArmyBounds[i].bounds);
                ArmyProvider.RegisterArmy(armyData);
            }

            ArmyProvider.FillUpEnemiesForRegisteredArmies();
        }

        private ArmyData SpawnArmy(string armyId, Bounds spawnBounds) // TODO simplify code
        {
            ArmySetup armySetup = battleInstaller.BattleSetup.TryFindArmySetupBy(armyId);
            List<Warrior> warriors = new();
            List<Archer> archers = new();

            if (armySetup != null)
            {
                for (int i = 0; i < armySetup.WarriorsCount; i++)
                {
                    Warrior spawnedWarrior = SpawnUnit(warriorPrefab, armySetup, spawnBounds);
                    warriors.Add(spawnedWarrior);
                }

                for (int i = 0; i < armySetup.WarriorsCount; i++)
                {
                    Archer spawnedArcher = SpawnUnit(archerPrefab, armySetup, spawnBounds);
                    archers.Add(spawnedArcher);
                }
            }
            else
            {
                Debug.LogError("Army Setup could not be found. Can not spawn army.");
            }

            return new ArmyData(armySetup, warriors, archers);
        }

        private T SpawnUnit<T>(T unitToSpawn, ArmySetup armySetup, Bounds spawnBounds) where T : BaseUnit
        {
            T spawnedUnit = Instantiate(unitToSpawn);
            spawnedUnit.Initialize(battleInstaller);

            spawnedUnit.transform.position = Utils.GetRandomPosInBounds(spawnBounds);
            spawnedUnit.armyId = armySetup.ArmyId;
            spawnedUnit.SetColor(armySetup.ArmyColor);

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

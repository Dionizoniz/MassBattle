using System.Collections.Generic;
using MassBattle.Core.Entities.Engine;
using MassBattle.Logic.Armies;
using MassBattle.Logic.Providers;
using MassBattle.Logic.Units;
using MassBattle.Logic.Utilities;
using UnityEngine;

namespace MassBattle.Logic.BattleCreator
{
    public class BattleSpawner : ExtendedMonoBehaviour, IBattleSpawner
    {
        private const string UNITS_ROOT_NAME = "UnitsRoot";

        [SerializeField]
        private Warrior _warriorPrefab;
        [SerializeField]
        private Archer _archerPrefab;

        [Space, SerializeField]
        private List<BoxCollider> _spawnArmyBounds = new();

        private IBattleSetup _battleSetup;
        private IArmyProvider _armyProvider;
        private IUpdateProvider _updateProvider;
        private IUnitsFactory _unitsFactory;
        private Transform _unitsRoot;

        private Vector3 _forwardTarget; // TODO improve solution - now is moved only

        public void Initialize(
                IBattleSetup battleSetup, IArmyProvider armyProvider, IUpdateProvider updateProvider,
                IUnitsFactory unitsFactory)
        {
            _updateProvider = updateProvider;
            _battleSetup = battleSetup;
            _armyProvider = armyProvider;
            _unitsFactory = unitsFactory;

            CreateUnitsRoot();
            SpawnArmies();
        }

        private void SpawnArmies()
        {
            List<string> armyIds = _battleSetup.FindAllArmySetupIds();
            _armyProvider.ClearArmies();

            for (int i = 0; i < armyIds.Count; i++)
            {
                // TODO add index protections
                ArmyData armyData = SpawnArmy(armyIds[i], _spawnArmyBounds[i].bounds);
                _armyProvider.RegisterArmy(armyData);
            }

            _armyProvider.FillUpEnemiesForRegisteredArmies();
        }

        private void CreateUnitsRoot()
        {
            _unitsRoot = new GameObject(UNITS_ROOT_NAME).transform;
        }

        private ArmyData SpawnArmy(string armyId, Bounds spawnBounds) // TODO simplify code
        {
            ArmySetup armySetup = _battleSetup.TryFindArmySetupBy(armyId);
            List<Warrior> warriors = new();
            List<Archer> archers = new();

            if (armySetup != null)
            {
                for (int i = 0; i < armySetup.WarriorsCount; i++)
                {
                    Warrior spawnedWarrior = SpawnUnit(_warriorPrefab, armySetup, spawnBounds);
                    warriors.Add(spawnedWarrior);
                }

                for (int i = 0; i < armySetup.ArchersCount; i++)
                {
                    Archer spawnedArcher = SpawnUnit(_archerPrefab, armySetup, spawnBounds);
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
            T spawnedUnit = Instantiate(unitToSpawn, _unitsRoot);

            spawnedUnit.Initialize(_armyProvider, armySetup, _updateProvider, _unitsFactory);
            spawnedUnit._transform.position = PositionFinder.FindRandomPositionIn(spawnBounds);

            return spawnedUnit;
        }

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
            // _forwardTarget = (mainCenter - Camera.main.transform.position).normalized;
            //
            // Camera.main.transform.forward += (_forwardTarget - Camera.main.transform.forward) * 0.1f;
        }
    }
}

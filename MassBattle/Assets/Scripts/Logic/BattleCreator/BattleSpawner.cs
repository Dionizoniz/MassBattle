using System.Collections.Generic;
using MassBattle.Core.Entities.Engine;
using MassBattle.Logic.Armies;
using MassBattle.Logic.Databases;
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
        private IColorDatabase _colorDatabase;
        private Transform _unitsRoot;

        public void Initialize(
                IBattleSetup battleSetup, IArmyProvider armyProvider, IUpdateProvider updateProvider,
                IUnitsFactory unitsFactory, IColorDatabase colorDatabase)
        {
            _colorDatabase = colorDatabase;
            _updateProvider = updateProvider;
            _battleSetup = battleSetup;
            _armyProvider = armyProvider;
            _unitsFactory = unitsFactory;

            CreateUnitsRoot();
            SpawnArmies();
        }

        private void CreateUnitsRoot()
        {
            _unitsRoot = new GameObject(UNITS_ROOT_NAME).transform;
        }

        private void SpawnArmies()
        {
            List<string> armyIds = _battleSetup.FindAllArmySetupIds();
            _armyProvider.ClearArmies();

            for (int armyIndex = 0, boundsIndex = 0; armyIndex < armyIds.Count; armyIndex++)
            {
                if (boundsIndex < _spawnArmyBounds.Count)
                {
                    ArmyData armyData = TrySpawnArmy(armyIds[armyIndex], _spawnArmyBounds[boundsIndex].bounds);

                    if (armyData != null)
                    {
                        _armyProvider.RegisterArmy(armyData);
                        boundsIndex++;
                    }
                }
                else
                {
                    Debug.LogError($"Not enough army bounds in BattleSpawner. Army {armyIds[armyIndex]} can not be spawned.");
                }
            }

            _armyProvider.InitializedRegisteredArmies();
        }

        private ArmyData TrySpawnArmy(string armyId, Bounds spawnBounds)
        {
            ArmySetup setup = _battleSetup.TryFindArmySetupBy(armyId);
            ArmyData armyData = null;

            if (setup != null)
            {
                if (setup.IsArmyActive)
                {
                    List<Warrior> warriors = SpawnUnits(_warriorPrefab, setup.WarriorsCount, setup, spawnBounds);
                    List<Archer> archers = SpawnUnits(_archerPrefab, setup.ArchersCount, setup, spawnBounds);
                    armyData = new ArmyData(setup, warriors, archers);
                }
            }
            else
            {
                Debug.LogError("Army Setup could not be found. Can not spawn army.");
            }

            return armyData;
        }

        private List<T> SpawnUnits<T>(T unitToSpawn, int unitsCount, ArmySetup armySetup, Bounds spawnBounds)
                where T : BaseUnit
        {
            List<T> spawnedUnits = new();

            for (int i = 0; i < unitsCount; i++)
            {
                T spawnUnit = SpawnUnit(unitToSpawn, armySetup, spawnBounds);
                spawnedUnits.Add(spawnUnit);
            }

            return spawnedUnits;
        }

        private T SpawnUnit<T>(T unitToSpawn, ArmySetup armySetup, Bounds spawnBounds) where T : BaseUnit
        {
            T spawnedUnit = Instantiate(unitToSpawn, _unitsRoot);

            spawnedUnit.Initialize(armySetup, _armyProvider, _updateProvider, _unitsFactory, _colorDatabase);
            spawnedUnit._transform.position = PositionFinder.FindRandomPositionIn(spawnBounds);

            return spawnedUnit;
        }
    }
}

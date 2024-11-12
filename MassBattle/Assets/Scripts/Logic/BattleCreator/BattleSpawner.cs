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
        private ColorDatabase _colorDatabase;
        private Transform _unitsRoot;

        public void Initialize(
                IBattleSetup battleSetup, IArmyProvider armyProvider, IUpdateProvider updateProvider,
                IUnitsFactory unitsFactory, ColorDatabase colorDatabase)
        {
            _colorDatabase = colorDatabase;
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
                if (i < _spawnArmyBounds.Count)
                {
                    ArmyData armyData = SpawnArmy(armyIds[i], _spawnArmyBounds[i].bounds);
                    _armyProvider.RegisterArmy(armyData);
                }
                else
                {
                    Debug.LogError($"Not enough army bounds in BattleSpawner. Army {armyIds[i]} can not be spawned.");
                }
            }

            _armyProvider.InitializedRegisteredArmies();
        }

        private void CreateUnitsRoot()
        {
            _unitsRoot = new GameObject(UNITS_ROOT_NAME).transform;
        }

        private ArmyData SpawnArmy(string armyId, Bounds spawnBounds)
        {
            ArmySetup armySetup = _battleSetup.TryFindArmySetupBy(armyId);
            List<Warrior> warriors = new();
            List<Archer> archers = new();

            if (armySetup != null)
            {
                warriors = SpawnUnits(_warriorPrefab, armySetup.WarriorsCount, armySetup, spawnBounds);
                archers = SpawnUnits(_archerPrefab, armySetup.ArchersCount, armySetup, spawnBounds);
            }
            else
            {
                Debug.LogError("Army Setup could not be found. Can not spawn army.");
            }

            return new ArmyData(armySetup, warriors, archers);
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

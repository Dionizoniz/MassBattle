using System.Collections.Generic;
using System.Linq;
using MassBattle.Core.Entities.Engine;
using MassBattle.Core.Entities.Tests;
using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.Armies;
using MassBattle.Logic.Databases.ArmyDatabase;
using MassBattle.Logic.Databases.Colors;
using MassBattle.Logic.Providers;
using MassBattle.Logic.Units;
using MassBattle.Logic.Utilities;
using UnityEngine;

namespace MassBattle.Logic.BattleCreator
{
    // TODO change name to ArmySpawner and split logic of load art scene to other system
    public class BattleSpawner : ExtendedMonoBehaviour, IBattleSpawner, ICheckSetup
    {
        private const string UNITS_ROOT_NAME = "UnitsRoot";

        [SerializeField]
        private Warrior _warriorPrefab;
        [SerializeField]
        private Archer _archerPrefab;

        [Space, SerializeField]
        private List<BoxCollider> _spawnArmyBounds = new();

        public int SpawnArmyBoundsCount => _spawnArmyBounds.Count;

        private IArmyDatabase _armyDatabase;
        private IArmyProvider _armyProvider;
        private IUpdateProvider _updateProvider;
        private IUnitsFactory _unitsFactory;
        private IColorDatabase _colorDatabase;
        private Transform _unitsRoot;
        private SceneLoader _sceneLoader;

        public void Initialize(
                IArmyDatabase armyDatabase, IArmyProvider armyProvider, IUpdateProvider updateProvider,
                IUnitsFactory unitsFactory, IColorDatabase colorDatabase, SceneLoader sceneLoader)
        {
            _armyDatabase = armyDatabase;
            _armyProvider = armyProvider;
            _updateProvider = updateProvider;
            _unitsFactory = unitsFactory;
            _colorDatabase = colorDatabase;
            _sceneLoader = sceneLoader;

            LoadArtScene();
            CreateUnitsRoot();
            SpawnArmies();
        }

        private void LoadArtScene()
        {
            _sceneLoader.LoadRandomArtScene();
        }

        private void CreateUnitsRoot()
        {
            _unitsRoot = new GameObject(UNITS_ROOT_NAME).transform;
        }

        private void SpawnArmies()
        {
            _armyProvider.ClearArmies();

            for (int armyIndex = 0, boundsIndex = 0; armyIndex < _armyDatabase.ArmiesData.Count; armyIndex++)
            {
                InitialArmyData initialArmyData = _armyDatabase.ArmiesData[armyIndex];

                if (boundsIndex < _spawnArmyBounds.Count)
                {
                    ArmyData armyData = TrySpawnArmy(initialArmyData, _spawnArmyBounds[boundsIndex].bounds);

                    if (armyData != null)
                    {
                        _armyProvider.RegisterArmy(armyData);
                        boundsIndex++;
                    }
                }
                else
                {
                    Debug.LogError($"Not enough army bounds in BattleSpawner. Army {initialArmyData.ArmyName} can not be spawned.");
                }
            }

            _armyProvider.InitializedRegisteredArmies();
        }

        private ArmyData TrySpawnArmy(InitialArmyData initialArmyData, Bounds spawnBounds)
        {
            ArmyData armyData = null;

            if (initialArmyData.IsArmyActive)
            {
                List<Warrior> warriors = SpawnUnits(_warriorPrefab, initialArmyData.UnitsCountSetup["0"],
                                                    initialArmyData, spawnBounds);

                List<Archer> archers = SpawnUnits(_archerPrefab, initialArmyData.UnitsCountSetup["1"], initialArmyData,
                                                  spawnBounds);

                armyData = new ArmyData(initialArmyData, warriors, archers);
            }

            return armyData;
        }

        private List<T> SpawnUnits<T>(
                T unitToSpawn, int unitsCount, InitialArmyData initialArmyData, Bounds spawnBounds)
                where T : BaseUnit
        {
            List<T> spawnedUnits = new();

            for (int i = 0; i < unitsCount; i++)
            {
                T spawnUnit = SpawnUnit(unitToSpawn, initialArmyData, spawnBounds);
                spawnedUnits.Add(spawnUnit);
            }

            return spawnedUnits;
        }

        private T SpawnUnit<T>(T unitToSpawn, InitialArmyData initialArmyData, Bounds spawnBounds) where T : BaseUnit
        {
            T spawnedUnit = Instantiate(unitToSpawn, _unitsRoot);

            spawnedUnit.Initialize(initialArmyData, _armyProvider, _updateProvider, _unitsFactory, _colorDatabase);
            spawnedUnit._transform.position = PositionFinder.FindRandomPositionIn(spawnBounds);

            return spawnedUnit;
        }

        public bool IsSetupCorrect()
        {
            bool isSetupCorrect = _spawnArmyBounds.Any();
            isSetupCorrect &= _warriorPrefab != null;
            isSetupCorrect &= _archerPrefab != null;

            return isSetupCorrect;
        }
    }
}

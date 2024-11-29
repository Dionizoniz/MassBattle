using System.Collections.Generic;
using System.Linq;
using MassBattle.Core.Engine;
using MassBattle.Core.Entities;
using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.Armies;
using MassBattle.Logic.Databases.Armies;
using MassBattle.Logic.Databases.Colors;
using MassBattle.Logic.Databases.Units;
using MassBattle.Logic.Providers;
using MassBattle.Logic.Units;
using MassBattle.Logic.Utilities;
using UnityEngine;

namespace MassBattle.Logic.BattleCreator
{
    // TODO Future feature - change name to ArmySpawner and split logic of load art scene to other system
    public class BattleSpawner : ExtendedMonoBehaviour, IBattleSpawner, ICheckSetup
    {
        private const string UNITS_ROOT_NAME = "UnitsRoot";

        [Space, SerializeField]
        private List<BoxCollider> _spawnArmyBounds = new();

        public int SpawnArmyBoundsCount => _spawnArmyBounds.Count;

        private IArmyDatabase _armyDatabase;
        private IArmyProvider _armyProvider;
        private IUpdateProvider _updateProvider;
        private IUnitsFactory _unitsFactory;
        private IColorDatabase _colorDatabase;
        private Transform _unitsRoot;
        private ISceneLoader _sceneLoader;
        private IUnitDatabase _unitDatabase;

        public void Initialize(
                IArmyDatabase armyDatabase, IArmyProvider armyProvider, IUpdateProvider updateProvider,
                IUnitsFactory unitsFactory, IColorDatabase colorDatabase, ISceneLoader sceneLoader,
                IUnitDatabase unitDatabase)
        {
            _armyDatabase = armyDatabase;
            _armyProvider = armyProvider;
            _updateProvider = updateProvider;
            _unitsFactory = unitsFactory;
            _colorDatabase = colorDatabase;
            _sceneLoader = sceneLoader;
            _unitDatabase = unitDatabase;

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
                    Debug.LogError($"Not enough army bounds in BattleSpawner. Army {initialArmyData.Name} can not be spawned.");
                }
            }

            _armyProvider.InitializedRegisteredArmies();
        }

        private ArmyData TrySpawnArmy(InitialArmyData initialArmyData, Bounds spawnBounds)
        {
            ArmyData armyData = null;

            if (initialArmyData.IsArmyActive)
            {
                Dictionary<string, List<BaseUnit>> spawnedUnits = new();

                foreach (var unitSetup in FindUnitsCountSetup(initialArmyData))
                {
                    UnitDescriptor unitDescriptor = _unitDatabase.TryFindElementBy(unitSetup.Key);
                    List<BaseUnit> units = SpawnUnits(unitDescriptor, unitSetup.Value, initialArmyData, spawnBounds);

                    spawnedUnits.Add(unitSetup.Key, units);
                }

                armyData = new ArmyData(initialArmyData, spawnedUnits);
            }

            return armyData;
        }

        private Dictionary<string, int> FindUnitsCountSetup(InitialArmyData initialArmyData)
        {
            Dictionary<string, int> setup = initialArmyData.UnitsCountSetup;
            return setup ?? _unitDatabase.GenerateDefaultUnitsCountSetup(initialArmyData.DefaultUnitStackSize);
        }

        private List<BaseUnit> SpawnUnits(
                UnitDescriptor unitDescriptor, int unitsCount, InitialArmyData initialArmyData, Bounds spawnBounds)
        {
            List<BaseUnit> spawnedUnits = new();

            for (int i = 0; i < unitsCount; i++)
            {
                BaseUnit spawnUnit = SpawnUnit(unitDescriptor, initialArmyData, spawnBounds);
                spawnedUnits.Add(spawnUnit);
            }

            return spawnedUnits;
        }

        private BaseUnit SpawnUnit(UnitDescriptor unitDescriptor, InitialArmyData initialArmyData, Bounds spawnBounds)
        {
            BaseUnit spawnedUnit = Instantiate(unitDescriptor.UnitPrefabToSpawn, _unitsRoot);

            string id = unitDescriptor.DescriptorId;
            spawnedUnit.Initialize(id, initialArmyData, _armyProvider, _updateProvider, _unitsFactory, _colorDatabase);
            spawnedUnit._transform.position = PositionFinder.FindRandomPositionIn(spawnBounds);

            return spawnedUnit;
        }

        public bool IsSetupCorrect()
        {
            return _spawnArmyBounds.Any();
        }
    }
}

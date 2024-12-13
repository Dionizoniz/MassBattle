using System;
using System.Collections.Generic;
using System.Linq;
using MassBattle.Core.Engine;
using MassBattle.Core.Entities;
using MassBattle.Core.Providers;
using MassBattle.Logic.Armies;
using MassBattle.Logic.Battle.Setup;
using MassBattle.Logic.Databases.Units;
using MassBattle.Logic.Units;
using MassBattle.Logic.Utilities;
using UnityEngine;
using Zenject;

namespace MassBattle.Logic.Battle.Spawner
{
    public class ArmySpawner : ExtendedMonoBehaviour, IArmySpawner, ICheckSetup, ISceneSpawner
    {
        public event Action OnSpawnScene = delegate { };

        [Space, SerializeField]
        private List<BoxCollider> _spawnArmyBounds = new();

        public int SpawnArmyBoundsCount => _spawnArmyBounds.Count;
        public bool IsSceneSpawned { get; private set; }

        private IBattleSetup _battleSetup;
        private IArmyProvider _armyProvider;
        private IUnitDatabase _unitDatabase;
        private DiContainer _container;

        private Transform _unitsRoot;

        [Inject]
        private void Construct(
                IBattleSetup battleSetup, IArmyProvider armyProvider, IUnitDatabase unitDatabase,
                DiContainer container)
        {
            _battleSetup = battleSetup;
            _armyProvider = armyProvider;
            _unitDatabase = unitDatabase;
            _container = container;
        }

        private void Awake()
        {
            CreateUnitsRoot();
            SpawnArmies();
        }

        private void CreateUnitsRoot()
        {
            _unitsRoot = new GameObject(ConstantValues.UNITS_ROOT_NAME).transform;
        }

        private void SpawnArmies()
        {
            ClearCurrentArmies();
            TrySpawnArmiesInBounds();
            InitializedRegisteredArmies();
            FinalizeArmySpawn();
        }

        private void ClearCurrentArmies()
        {
            _armyProvider.ClearArmies();
        }

        private void TrySpawnArmiesInBounds()
        {
            int armyCount = _battleSetup.SavedArmiesData.Count;
            int boundsCount = _spawnArmyBounds.Count;

            for (int armyIndex = 0, boundsIndex = 0; armyIndex < armyCount && boundsIndex < boundsCount; armyIndex++)
            {
                InitialArmyData initialArmyData = _battleSetup.SavedArmiesData[armyIndex];
                ArmyData armyData = TrySpawnArmy(initialArmyData, _spawnArmyBounds[boundsIndex].bounds);

                if (armyData != null)
                {
                    _armyProvider.RegisterArmy(armyData);
                    boundsIndex++;
                }
            }

            if (armyCount > boundsCount)
            {
                Debug.LogError("Not enough army bounds in BattleSpawner. Some Armies can not be spawned!!!");
            }
        }

        private ArmyData TrySpawnArmy(InitialArmyData initialArmyData, Bounds spawnBounds)
        {
            ArmyData armyData = null;

            if (initialArmyData.IsArmyActive)
            {
                Dictionary<string, List<BaseUnit>> spawnedUnits = new();

                foreach (KeyValuePair<string, int> unitSetup in FindUnitsCountSetup(initialArmyData))
                {
                    List<BaseUnit> units = SpawnUnits(unitSetup, initialArmyData, spawnBounds);
                    spawnedUnits.Add(unitSetup.Key, units);
                }

                armyData = new ArmyData(initialArmyData, spawnedUnits);
            }

            return armyData;
        }

        private Dictionary<string, int> FindUnitsCountSetup(InitialArmyData initialArmyData)
        {
            return initialArmyData.UnitsCountSetup ?? _unitDatabase.GenerateDefaultUnitsCountSetup(initialArmyData);
        }

        private List<BaseUnit> SpawnUnits(
                KeyValuePair<string, int> unitSetup, InitialArmyData initialArmyData, Bounds spawnBounds)
        {
            List<BaseUnit> spawnedUnits = new();
            UnitDescriptor unitDescriptor = _unitDatabase.TryFindElementBy(unitSetup.Key);
            int unitsCount = unitSetup.Value;

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
            _container.InjectGameObject(spawnedUnit.gameObject);

            spawnedUnit.Initialize(unitDescriptor.DescriptorId, initialArmyData);
            spawnedUnit._transform.position = PositionFinder.FindRandomPositionIn(spawnBounds);

            return spawnedUnit;
        }

        private void InitializedRegisteredArmies()
        {
            _armyProvider.InitializedRegisteredArmies();
        }

        private void FinalizeArmySpawn()
        {
            IsSceneSpawned = true;
            OnSpawnScene.Invoke();
        }

        public bool IsSetupCorrect()
        {
            return _spawnArmyBounds.Any();
        }
    }
}

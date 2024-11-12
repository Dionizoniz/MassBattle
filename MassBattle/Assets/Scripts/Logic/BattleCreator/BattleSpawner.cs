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
                if (i < _spawnArmyBounds.Count)
                {
                    ArmyData armyData = SpawnArmy(armyIds[i], _spawnArmyBounds[i].bounds);
                    _armyProvider.RegisterArmy(armyData);
                }
                else
                {
                    Debug.LogError("Not enough army bounds in BattleSpawner. Army can not be spawned.");
                }
            }

            _armyProvider.InitializedRegisteredArmies();
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
    }
}

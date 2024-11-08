using MassBattle.Logic.Armies;
using MassBattle.Logic.BattleCreator;
using MassBattle.Logic.Setup;
using UnityEngine;

namespace MassBattle.Logic.Installers
{
    public class BattleInstaller : MonoBehaviour, IBattleInstaller
    {
        [SerializeField]
        private BattleSpawner _battleSpawnerToSpawn;
        [SerializeField]
        private BattleSetup _battleSetup;

        private IBattleSpawner _battleSpawner;
        private IArmyProvider _armyProvider;

        private void Awake()
        {
            _battleSpawner = Instantiate(_battleSpawnerToSpawn);
            _armyProvider = new ArmyProvider();
        }

        private void Start()
        {
            _battleSpawner.Initialize(_battleSetup, _armyProvider);
        }
    }
}

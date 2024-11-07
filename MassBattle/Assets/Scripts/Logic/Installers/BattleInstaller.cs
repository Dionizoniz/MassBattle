using MassBattle.Logic.Armies;
using MassBattle.Logic.BattleCreator;
using MassBattle.Logic.Setup;
using UnityEngine;

namespace MassBattle.Logic.Installers
{
    public class BattleInstaller : MonoBehaviour, IBattleInstaller
    {
        [SerializeField]
        private BattleSpawner _battleSpawner;
        [SerializeField]
        private BattleSetup _battleSetup;

        public IBattleSpawner BattleSpawner { get; private set; }
        public IBattleSetup BattleSetup => _battleSetup;

        public IArmyProvider ArmyProvider { get; private set; }

        private void Awake()
        {
            BattleSpawner = Instantiate(_battleSpawner);
            ArmyProvider = new ArmyProvider();
        }

        private void Start()
        {
            BattleSpawner.Initialize(this);
        }
    }
}

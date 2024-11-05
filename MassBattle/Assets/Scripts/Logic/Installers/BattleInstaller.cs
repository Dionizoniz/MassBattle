using MassBattle.Logic.BattleCreator;
using UnityEngine;

namespace MassBattle.Logic.Installers
{
    public class BattleInstaller : MonoBehaviour, IBattleInstaller
    {
        [SerializeField]
        private BattleSpawner battleSpawner;

        public IBattleSpawner BattleSpawner { get; private set; }

        private void Start()
        {
            BattleSpawner = Instantiate(battleSpawner);
            BattleSpawner.Initialize(this);
        }

        // BattleArmiesProvider
        // Pooling ???
        // summary screen
    }
}

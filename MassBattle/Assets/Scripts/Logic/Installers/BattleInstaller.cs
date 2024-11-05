using MassBattle.Logic.BattleCreator;
using UnityEngine;

namespace MassBattle.Logic.Installers
{
    public class BattleInstaller : MonoBehaviour, IBattleInstaller
    {
        [SerializeField]
        private BattleSpawner battleSpawner;

        public IBattleSpawner BattleSpawner { get; private set; }

        private void Awake()
        {
            BattleSpawner = Instantiate(battleSpawner);
        }

        // BattleArmiesProvider
        // Pooling ???
        // summary screen
    }
}

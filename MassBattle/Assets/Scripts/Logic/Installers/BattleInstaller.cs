using System;
using MassBattle.Logic.Armies;
using MassBattle.Logic.BattleCreator;
using UnityEngine;

namespace MassBattle.Logic.Installers
{
    public class BattleInstaller : MonoBehaviour, IBattleInstaller
    {
        [SerializeField]
        private BattleSpawner battleSpawner;

        public IBattleSpawner BattleSpawner { get; private set; }
        public IArmyProvider ArmyProvider { get; private set; }

        private void Awake()
        {
            BattleSpawner = Instantiate(battleSpawner);
            ArmyProvider = new ArmyProvider();
        }

        private void Start()
        {
            BattleSpawner.Initialize(this);
        }

        // Pooling ???
        // summary screen
    }
}

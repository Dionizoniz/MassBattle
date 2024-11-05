using MassBattle.Logic.Installers;
using MassBattle.Logic.Units;
using UnityEngine;

namespace MassBattle.Logic.BattleCreator
{
    public class BattleSpawner : MonoBehaviour, IBattleSpawner, IInitialize
    {
        [SerializeField]
        private ArmyModelSO army1Model;

        [SerializeField]
        private ArmyModelSO army2Model;

        [SerializeField]
        private Warrior warriorPrefab;

        [SerializeField]
        private Archer archerPrefab;

        [SerializeField]
        private BoxCollider leftArmySpawnBounds;

        [SerializeField]
        private BoxCollider rightArmySpawnBounds;

        private IBattleInstaller battleInstaller;

        public void Initialize(IBattleInstaller battleInstaller)
        {
            this.battleInstaller = battleInstaller;
        }
        
        
    }
}

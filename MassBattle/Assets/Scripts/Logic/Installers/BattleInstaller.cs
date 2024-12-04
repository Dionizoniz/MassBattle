using MassBattle.Core.Installers;
using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Core.UserInput;
using MassBattle.Logic.Armies;
using MassBattle.Logic.BattleCreator;
using MassBattle.Logic.Controllers;
using MassBattle.Logic.Databases.Armies;
using MassBattle.Logic.Databases.Colors;
using MassBattle.Logic.Databases.Units;
using MassBattle.Logic.Providers;
using UnityEngine;

namespace MassBattle.Logic.Installers
{
    public class BattleInstaller : BaseInstaller, IBattleInstaller
    {
        [Space, SerializeField]
        private Transform _cameraControllerRoot;
        [SerializeField]
        private BattleCamera _battleCameraToSpawn;

        private IBattleCamera _battleCamera;

        private void Awake()
        {
            SpawnSystems();
        }

        private void SpawnSystems()
        {
            _battleCamera = Instantiate(_battleCameraToSpawn, _cameraControllerRoot);
            AdjustCameraControllerRoot();
        }

        private void AdjustCameraControllerRoot()
        {
            _cameraControllerRoot.SetParent(null);
        }

        public override bool IsSetupCorrect()
        {
            return true;
        }
    }
}

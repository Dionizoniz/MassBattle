using MassBattle.Core.Entities.Installers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.Installers;
using MassBattle.UI.EndBattlePanel;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MassBattle.UI.Installers
{
    public class BattleUiInstaller : BaseInstaller, IBattleUiInstaller
    {
        [SerializeField]
        private BattleInstaller _spawnedBattleInstaller;
        [SerializeField]
        private SceneLoader _sceneLoader;

        [SerializeField]
        private EndBattlePanelController _endBattlePanelControllerToSpawn;
        [SerializeField]
        private EventSystem _eventSystemToSpawn;

        private IEndBattlePanelController _endBattlePanel;

        private void Awake()
        {
            SpawnEndBattlePanelController();
            SpawnEventSystem();
        }

        private void SpawnEndBattlePanelController()
        {
            _endBattlePanel = Instantiate(_endBattlePanelControllerToSpawn);
        }

        private void SpawnEventSystem()
        {
            Instantiate(_eventSystemToSpawn);
        }

        private void Start()
        {
            InjectData();
        }

        private void InjectData()
        {
            _endBattlePanel.InjectData(_spawnedBattleInstaller.ArmyProvider, _sceneLoader);
        }

        public override bool IsSetupCorrect()
        {
            bool isSetupCorrect = true;

            isSetupCorrect &= _spawnedBattleInstaller != null;
            isSetupCorrect &= _sceneLoader != null;
            isSetupCorrect &= _endBattlePanelControllerToSpawn != null;
            isSetupCorrect &= _eventSystemToSpawn != null;

            return isSetupCorrect;
        }
    }
}

using MassBattle.Core.Entities.Installers;
using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Core.UserInput;
using MassBattle.Logic.Installers;
using MassBattle.UI.EndBattlePanel;
using MassBattle.UI.PauseMenuPanel;
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

        [Space, SerializeField]
        private EndBattlePanelController _endBattlePanelControllerToSpawn;
        [SerializeField]
        private PauseMenuPanelController _pauseMenuPanelControllerToSpawn;
        [SerializeField]
        private EventSystem _eventSystemToSpawn;

        private IPauseGameProvider _pauseGameProvider;
        private IEndBattlePanelController _endBattlePanel;
        private IPauseMenuPanelController _pauseMenuPanel;

        private void Awake()
        {
            SpawnPanelControllers();
            SpawnEventSystem();
            CreateInstances();
        }

        private void SpawnPanelControllers()
        {
            _endBattlePanel = Instantiate(_endBattlePanelControllerToSpawn);
            _pauseMenuPanel = Instantiate(_pauseMenuPanelControllerToSpawn);
        }

        private void SpawnEventSystem()
        {
            Instantiate(_eventSystemToSpawn);
        }

        private void CreateInstances()
        {
            _pauseGameProvider = new PauseGameProvider();
        }

        private void Start()
        {
            InjectData();
        }

        private void InjectData()
        {
            _endBattlePanel.InjectData(_spawnedBattleInstaller.ArmyProvider, _sceneLoader);
            _pauseMenuPanel.InjectData(_spawnedBattleInstaller.InputFacade, _sceneLoader, _pauseGameProvider);
        }

        public override bool IsSetupCorrect()
        {
            bool isSetupCorrect = true;

            isSetupCorrect &= _spawnedBattleInstaller == null; // INFO: we referenced in on scene to avoid FindObject
            isSetupCorrect &= _sceneLoader != null;
            isSetupCorrect &= _endBattlePanelControllerToSpawn != null;
            isSetupCorrect &= _eventSystemToSpawn != null;

            return isSetupCorrect;
        }
    }
}

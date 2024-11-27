using MassBattle.Core.Installers;
using MassBattle.Core.Providers;
using MassBattle.Logic.Installers;
using MassBattle.UI.EndBattlePanel;
using MassBattle.UI.FadePanel;
using MassBattle.UI.FadePanel.Executors;
using MassBattle.UI.PauseMenuPanel;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MassBattle.UI.Installers
{
    public class BattleUiInstaller : BaseInstaller, IBattleUiInstaller
    {
        [SerializeField]
        private BattleInstaller _spawnedBattleInstaller;

        [Space, SerializeField]
        private EndBattlePanelController _endBattlePanelControllerToSpawn;
        [SerializeField]
        private PauseMenuPanelController _pauseMenuPanelControllerToSpawn;
        [SerializeField]
        private FadePanelController _fadePanelControllerToSpawn;
        [SerializeField]
        private EventSystem _eventSystemToSpawn;

        private IBattleInstaller BaseInstaller => _spawnedBattleInstaller;

        private IEndBattlePanelController _endBattlePanel;
        private IPauseMenuPanelController _pauseMenuPanel;
        private IFadePanelController _fadePanel;

        private IPauseGameProvider _pauseGameProvider;
        private IFadeExecutor _fadeExecutor;

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
            _fadePanel = Instantiate(_fadePanelControllerToSpawn);
        }

        private void SpawnEventSystem()
        {
            Instantiate(_eventSystemToSpawn);
        }

        private void CreateInstances()
        {
            _pauseGameProvider = new PauseGameProvider();
            _fadeExecutor = new FadeExecutor(_fadePanel, this);
        }

        protected override void Start()
        {
            InjectData();

            base.Start();
        }

        private void InjectData()
        {
            _endBattlePanel.InjectData(BaseInstaller.ArmyProvider, BaseInstaller.SceneLoader, _pauseGameProvider);

            _pauseMenuPanel.InjectData(BaseInstaller.InputFacade, BaseInstaller.SceneLoader, _pauseGameProvider,
                                       BaseInstaller.ArmyProvider);
        }

        public override bool IsSetupCorrect()
        {
            bool isSetupCorrect = true;

            isSetupCorrect &= _spawnedBattleInstaller == null; // INFO: we referenced in on scene to avoid FindObject
            isSetupCorrect &= _endBattlePanelControllerToSpawn != null;
            isSetupCorrect &= _pauseMenuPanelControllerToSpawn != null;
            isSetupCorrect &= _eventSystemToSpawn != null;

            return isSetupCorrect;
        }
    }
}

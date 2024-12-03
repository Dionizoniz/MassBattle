using MassBattle.Core.Installers;
using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.Databases.Armies;
using MassBattle.Logic.Databases.Colors;
using MassBattle.Logic.Databases.Units;
using MassBattle.UI.FadePanel;
using MassBattle.UI.FadePanel.Executors;
using MassBattle.UI.LaunchMenu;
using UnityEngine;
using Zenject;

namespace MassBattle.UI.Installers
{
    public class LaunchMenuInstaller : BaseInstaller, ILaunchMenuInstaller
    {
        [SerializeField]
        private FadePanelController _fadePanelControllerToSpawn;

        [Space, SerializeField]
        private ArmyDatabase _armyDatabase;
        [SerializeField]
        private ColorDatabase _colorDatabase;
        [SerializeField]
        private UnitDatabase _unitDatabase;
        [SerializeField]
        private SceneLoader _sceneLoader;

        private IFadePanelController _fadePanel;

        private IFadeExecutor _fadeExecutor;

        private void Awake()
        {
            SpawnPanelControllers();
            CreateInstances();
        }

        private void SpawnPanelControllers()
        {
            _fadePanel = Instantiate(_fadePanelControllerToSpawn);
        }

        private void CreateInstances()
        {
            _fadeExecutor = new FadeExecutor(_fadePanel, this);
        }

        public override bool IsSetupCorrect()
        {
            bool isSetupCorrect = true;

            isSetupCorrect &= _armyDatabase != null;
            isSetupCorrect &= _colorDatabase != null;
            isSetupCorrect &= _sceneLoader != null;

            return isSetupCorrect;
        }
    }
}

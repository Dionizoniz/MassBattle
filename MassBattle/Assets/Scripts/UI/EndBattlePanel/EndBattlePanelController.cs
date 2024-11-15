using MassBattle.Core.Entities.MVC;
using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.Armies;

namespace MassBattle.UI.EndBattlePanel
{
    public class EndBattlePanelController : Controller<EndBattlePanelModel, EndBattlePanelView>,
                                            IEndBattlePanelController
    {
        private IArmyProvider _armyProvider;
        private ISceneLoader _sceneLoader;
        private IPauseGameProvider _pauseGameProvider;

        public void InjectData(
                IArmyProvider armyProvider, ISceneLoader sceneLoader, IPauseGameProvider pauseGameProvider)
        {
            _armyProvider = armyProvider;
            _sceneLoader = sceneLoader;
            _pauseGameProvider = pauseGameProvider;
        }

        protected override void Initialize()
        {
            base.Initialize();

            _view.Initialize();
            _model.InjectData(_armyProvider, _pauseGameProvider);
        }

        public void LoadLaunchMenuScene()
        {
            _model.ResumeGame();
            _sceneLoader.LoadLaunchMenuScene();
        }
    }
}

using MassBattle.Core.Entities.MVC;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.Armies;

namespace MassBattle.UI.EndBattlePanel
{
    public class EndBattlePanelController : Controller<EndBattlePanelModel, EndBattlePanelView>,
                                            IEndBattlePanelController
    {
        private IArmyProvider _armyProvider;
        private ISceneLoader _sceneLoader;

        public void InjectData(IArmyProvider armyProvider, ISceneLoader sceneLoader)
        {
            _armyProvider = armyProvider;
            _sceneLoader = sceneLoader;
        }

        protected override void Initialize()
        {
            base.Initialize();

            _view.Initialize();
            _model.InjectData(_armyProvider);
        }

        public void LoadLaunchMenuScene()
        {
            _sceneLoader.LoadLaunchMenuScene();
        }
    }
}

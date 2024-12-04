using MassBattle.Core.Patterns.MVC;
using MassBattle.Core.SceneLoaders;
using Zenject;

namespace MassBattle.UI.EndBattlePanel
{
    public class EndBattlePanelController : Controller<EndBattlePanelModel, EndBattlePanelView>,
                                            IEndBattlePanelController
    {
        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void LoadLaunchMenuScene()
        {
            _model.ResumeGame();
            _sceneLoader.LoadLaunchMenuScene();
        }
    }
}

using MassBattle.Core.Entities.MVC;

namespace MassBattle.UI.LoadingScenePanel
{
    public class LoadingScreenPanelController : Controller<LoadingScreenPanelModel, LoadingScreenPanelView>,
                                                ILoadingScreenPanelController
    {
        protected override void Initialize()
        {
            base.Initialize();
            _model.StartLoadingTargetScene();
        }
    }
}

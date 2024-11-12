using MassBattle.Core.Entities.MVC;
using MassBattle.Logic.Armies;
using UnityEngine.SceneManagement;

namespace MassBattle.UI.EndBattlePanel
{
    public class EndBattlePanelController : Controller<EndBattlePanelModel, EndBattlePanelView>,
                                            IEndBattlePanelController
    {
        private IArmyProvider _armyProvider;

        public void InjectData(IArmyProvider armyProvider)
        {
            _armyProvider = armyProvider;
        }

        protected override void Initialize()
        {
            base.Initialize();

            _view.Initialize();

            // TODO model inject data
        }

        public void LoadLaunchMenuScene()
        {
            SceneManager.LoadScene(0);
        }
    }
}

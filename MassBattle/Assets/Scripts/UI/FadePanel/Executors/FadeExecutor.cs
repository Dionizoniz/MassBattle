using MassBattle.Core.Entities;
using Zenject;

namespace MassBattle.UI.FadePanel.Executors
{
    public class FadeExecutor : IFadeExecutor
    {
        private IFadePanelController _fadePanelController;
        private ISceneSpawner _sceneSpawner;

        [Inject]
        private void Construct(IFadePanelController fadePanelController, ISceneSpawner sceneSpawner)
        {
            _fadePanelController = fadePanelController;
            _sceneSpawner = sceneSpawner;

            _fadePanelController.FadeInInstant();
            TryFadeOut();
        }

        private void TryFadeOut()
        {
            if (_sceneSpawner.IsSceneSpawned)
            {
                FadeOut();
            }
            else
            {
                AttachToEvents();
            }
        }

        private void FadeOut()
        {
            _fadePanelController.FadeOut();
        }

        private void AttachToEvents()
        {
            _sceneSpawner.OnSpawnScene += FadeOut;
        }

        ~FadeExecutor()
        {
            DetachFromEvents();
        }

        private void DetachFromEvents()
        {
            if (_sceneSpawner != null)
            {
                _sceneSpawner.OnSpawnScene -= FadeOut;
            }
        }
    }
}

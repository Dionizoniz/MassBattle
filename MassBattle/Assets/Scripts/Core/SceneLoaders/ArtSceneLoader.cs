using Zenject;

namespace MassBattle.Core.SceneLoaders
{
    public class ArtSceneLoader : IArtSceneLoader
    {
        private ISceneLoader _sceneLoader;

        [Inject]
        private void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;

            LoadArtScene();
        }

        private void LoadArtScene()
        {
            _sceneLoader.LoadRandomArtScene();
        }
    }
}

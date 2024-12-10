using UnityEngine;

namespace MassBattle.Core.SceneLoaders
{
    public interface ISceneLoader
    {
        void LoadLaunchMenuScene(bool useLoadingScreen = true);
        void LoadBattleScene(bool useLoadingScreen = true);
        void LoadRandomArtScene();
        AsyncOperation LoadTargetScene();
    }
}

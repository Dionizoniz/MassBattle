using System;
using MassBattle.Core.Entities.MVC;
using MassBattle.Core.SceneLoaders;
using UnityEngine;

namespace MassBattle.UI.LoadingScenePanel
{
    public class LoadingScreenPanelModel : Model<LoadingScreenPanelView>
    {
        [SerializeField]
        private SceneLoader _sceneLoader;

        public void StartLoadingTargetScene()
        {
            //_sceneLoader.TargetSceneNameToLoad
        }
    }
}

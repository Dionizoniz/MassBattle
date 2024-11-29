using MassBattle.Core.Patterns.MVC;
using MassBattle.Core.SceneLoaders;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MassBattle.UI.LoadingScenePanel
{
    public class LoadingScreenPanelModel : Model<LoadingScreenPanelView>
    {
        private const float SCENE_IS_READY_TO_LOAD_PROGRESS_VALUE = 0.9f;

        [SerializeField]
        private SceneLoader _sceneLoader;
        [SerializeField]
        private float _minLoadingTime = 1.5f;

        private AsyncOperation _loadSceneOperation;
        private float _loadingTime;

        public void StartLoadingTargetScene()
        {
            _loadSceneOperation = SceneManager.LoadSceneAsync(_sceneLoader.TargetSceneNameToLoad);
            _loadSceneOperation.allowSceneActivation = false;
        }

        private void Update()
        {
            UpdateLoadingTime();
            TryAllowSceneActivation();
        }

        private void UpdateLoadingTime()
        {
            _loadingTime += Time.deltaTime;
        }

        private void TryAllowSceneActivation()
        {
            if (_loadingTime > _minLoadingTime && _loadSceneOperation.progress >= SCENE_IS_READY_TO_LOAD_PROGRESS_VALUE)
            {
                _loadSceneOperation.allowSceneActivation = true;
            }
        }
    }
}

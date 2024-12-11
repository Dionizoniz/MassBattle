using System;
using MassBattle.Core.Patterns.MVC;
using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using UnityEngine;
using Zenject;

namespace MassBattle.UI.LoadingScenePanel
{
    public class LoadingScreenPanelModel : Model<LoadingScreenPanelView>
    {
        private const float SCENE_IS_READY_TO_LOAD_PROGRESS_VALUE = 0.9f;

        [SerializeField]
        private float _minLoadingTime = 1.5f;

        private ISceneLoader _sceneLoader;
        private IUpdateProvider _updateProvider;

        private AsyncOperation _loadSceneOperation;
        private float _loadingTime;

        [Inject]
        private void Construct(ISceneLoader sceneLoader, IUpdateProvider updateProvider)
        {
            _sceneLoader = sceneLoader;
            _updateProvider = updateProvider;

            _updateProvider.OnUpdate += ManualUpdate;
        }

        private void ManualUpdate()
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

        public void StartLoadingTargetScene()
        {
            _loadSceneOperation = _sceneLoader.LoadTargetScene();
            _loadSceneOperation.allowSceneActivation = false;
        }

        private void OnDestroy()
        {
            if (_updateProvider != null)
            {
                _updateProvider.OnUpdate -= ManualUpdate;
            }
        }
    }
}

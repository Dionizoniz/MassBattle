using System;
using System.Collections.Generic;
using MassBattle.Core.Engine;
using MassBattle.Core.Entities;
using MassBattle.Core.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MassBattle.Core.SceneLoaders
{
    [CreateAssetMenu(menuName = nameof(SceneLoader), fileName = nameof(SceneLoader), order = 0)]
    public class SceneLoader : ExtendedScriptableObject, ISceneLoader, ICheckSetup
    {
        [SerializeField]
        private SceneData _loadingSceneData;
        [SerializeField]
        private SceneData _launchMenuSceneData;
        [SerializeField]
        private SceneData _battleSceneData;

        [Space, SerializeField]
        private List<SceneData> _artScenesData = new();

        public string TargetSceneNameToLoad { get; private set; }

        public void LoadLaunchMenuScene(bool useLoadingScreen = true)
        {
            if (useLoadingScreen)
            {
                LoadScene(_launchMenuSceneData);
            }
            else
            {
                LoadSceneInstant(_launchMenuSceneData);
            }
        }

        private void LoadScene(SceneData sceneData)
        {
            TargetSceneNameToLoad = sceneData.SceneName;

            SceneManager.LoadScene(_loadingSceneData.SceneName);
        }

        private void LoadSceneInstant(SceneData sceneData)
        {
            SceneManager.LoadScene(sceneData.SceneName, LoadSceneMode.Single);
        }

        public void LoadBattleScene(bool useLoadingScreen = true)
        {
            if (useLoadingScreen)
            {
                LoadScene(_battleSceneData);
            }
            else
            {
                LoadSceneInstant(_battleSceneData);
            }
        }

        public void LoadRandomArtScene()
        {
            int randomIndex = UnityEngine.Random.Range(0, _artScenesData.Count);
            LoadAdditiveScene(_artScenesData[randomIndex]);
        }

        private void LoadAdditiveScene(SceneData sceneData)
        {
            SceneManager.LoadScene(sceneData.SceneName, LoadSceneMode.Additive);
        }

        private void OnValidate()
        {
            _loadingSceneData.CacheSceneName();
            _launchMenuSceneData.CacheSceneName();
            _battleSceneData.CacheSceneName();

            foreach (var sceneData in _artScenesData)
            {
                sceneData.CacheSceneName();
            }
        }

        public bool IsSetupCorrect()
        {
            bool isSetupCorrect = _loadingSceneData.IsSetupCorrect();
            isSetupCorrect &= _launchMenuSceneData.IsSetupCorrect();
            isSetupCorrect &= _battleSceneData.IsSetupCorrect();

            foreach (var sceneData in _artScenesData)
            {
                isSetupCorrect &= sceneData.IsSetupCorrect();
            }

            return isSetupCorrect;
        }

        [Serializable]
        private class SceneData : ICheckSetup
        {
            [SerializeField, ReadOnly]
            private string _sceneName;

#if UNITY_EDITOR
            [SerializeField]
            private SceneAsset _sceneAsset;
#endif

            public string SceneName => _sceneName;

            public void CacheSceneName()
            {
#if UNITY_EDITOR
                if (_sceneAsset != null)
                {
                    _sceneName = _sceneAsset.name;
                }
#endif
            }

            public bool IsSetupCorrect()
            {
                bool isSetupCorrect = string.IsNullOrEmpty(_sceneName) == false;

#if UNITY_EDITOR
                isSetupCorrect &= _sceneAsset != null;
#endif

                return isSetupCorrect;
            }
        }
    }
}

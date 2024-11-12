using System;
using MassBattle.Core.Entities.Engine;
using MassBattle.Core.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MassBattle.Core.SceneLoaders
{
    [CreateAssetMenu(menuName = "Create " + nameof(SceneLoader), fileName = nameof(SceneLoader), order = 0)]
    public class SceneLoader : ExtendedScriptableObject, ISceneLoader
    {
        [SerializeField]
        private SceneData _loadingSceneData;
        [SerializeField]
        private SceneData _launchMenuSceneData;
        [SerializeField]
        private SceneData _battleSceneData;

        public string TargetSceneNameToLoad { get; private set; }

        public void LoadLaunchMenuScene()
        {
            LoadScene(_launchMenuSceneData);
        }

        private void LoadScene(SceneData sceneData)
        {
            TargetSceneNameToLoad = sceneData.SceneName;

            SceneManager.LoadScene(_loadingSceneData.SceneName);
        }

        public void LoadBattleScene()
        {
            LoadScene(_battleSceneData);
        }

        private void OnValidate()
        {
            _loadingSceneData.CacheSceneName();
            _launchMenuSceneData.CacheSceneName();
            _battleSceneData.CacheSceneName();
        }

        [Serializable]
        private class SceneData
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
        }
    }
}

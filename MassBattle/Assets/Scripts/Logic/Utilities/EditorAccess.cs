using MassBattle.Core.Engine;
using MassBattle.Core.SceneLoaders;
using UnityEditor;
using UnityEngine;

namespace MassBattle.Logic.Utilities
{
    [CreateAssetMenu(menuName = nameof(EditorAccess), fileName = nameof(EditorAccess), order = 0)]
    public class EditorAccess : ExtendedScriptableObject
    {
        private const string PATH = "Assets/Content/Setup/EditorAccess.asset";

        [SerializeField]
        private SceneLoader _sceneLoader;

        public ISceneLoader SceneLoader => _sceneLoader;

        private static EditorAccess _instance;

        public static EditorAccess Instance
        {
            get
            {
                if (_instance == null)
                {
#if UNITY_EDITOR
                    _instance = AssetDatabase.LoadAssetAtPath(PATH, typeof(EditorAccess)) as EditorAccess;
#endif
                }

                return _instance;
            }
        }
    }
}

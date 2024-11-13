using MassBattle.Core.SceneLoaders;
using UnityEditor;
using UnityEngine;

namespace MassBattle.Tests.Editor
{
    [CreateAssetMenu(menuName = nameof(EditorAccess), fileName = nameof(EditorAccess), order = 0)]
    public class EditorAccess : ScriptableSingleton<EditorAccess>
    {
        [SerializeField]
        private SceneLoader _sceneLoader;

        public ISceneLoader SceneLoader => _sceneLoader;
    }
}

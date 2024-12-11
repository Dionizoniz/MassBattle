using MassBattle.Core.Engine;
using MassBattle.Core.Providers;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.Databases.Colors;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace MassBattle.Logic.Utilities
{
    [CreateAssetMenu(menuName = ConstantValues.SETUP_MENU_GROUP + nameof(EditorAccess), fileName = nameof(EditorAccess),
                     order = ConstantValues.SETUP_MENU_ORDER)]
    public class EditorAccess : ExtendedScriptableObject
    {
        private const string PATH = "Assets/Content/Setup/EditorAccess.asset";

        [SerializeField]
        private SceneLoader _sceneLoader;
        [SerializeField]
        private ColorDatabase _colorDatabase;

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

        public ValueDropdownList<string> PossibleColorsDropdown() => _colorDatabase.PossibleDescriptorsDropdown;
    }
}

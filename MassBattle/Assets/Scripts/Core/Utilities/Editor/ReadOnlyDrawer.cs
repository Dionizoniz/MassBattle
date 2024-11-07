using UnityEditor;
using UnityEngine;

namespace MassBattle.Core.Utilities.Editor
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        private bool _guiState;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            CacheGuiState();
            DisablingPropertyEdit();

            EditorGUI.PropertyField(position, property, label);

            RestoreGuiState();
        }

        private void CacheGuiState()
        {
            _guiState = GUI.enabled;
        }

        private void DisablingPropertyEdit()
        {
            GUI.enabled = false;
        }

        private void RestoreGuiState()
        {
            GUI.enabled = _guiState;
        }
    }
}

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MassBattle.Core.Providers
{
    public class ExitGameProvider : IExitGameProvider
    {
        public void ExitGame()
        {
            if (Application.isEditor)
            {
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#endif
            }
            else
            {
                Application.Quit();
            }
        }
    }
}

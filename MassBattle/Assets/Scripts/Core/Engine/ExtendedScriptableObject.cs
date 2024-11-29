using UnityEngine;

namespace MassBattle.Core.Engine
{
    public class ExtendedScriptableObject : ScriptableObject
    {
        private void Awake()
        {
            hideFlags = HideFlags.DontUnloadUnusedAsset;
        }
    }
}

using UnityEngine;

namespace MassBattle.Core.Entities.Engine
{
    public class ExtendedScriptableObject : ScriptableObject
    {
        private void Awake()
        {
            hideFlags = HideFlags.DontUnloadUnusedAsset;
        }
    }
}

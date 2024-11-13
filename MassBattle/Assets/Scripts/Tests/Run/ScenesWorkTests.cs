using System.Collections;
using MassBattle.Tests.Editor;
using UnityEngine.TestTools;

namespace MassBattle.Tests.Run
{
    public class ScenesWorkTests
    {
        [UnityTest]
        public IEnumerator _00_RunSceneWithoutErrors_LaunchMenuScene()
        {
            EditorAccess.instance.SceneLoader.LoadLaunchMenuScene(false);
            yield return null;
        }

        [UnityTest]
        public IEnumerator _01_RunSceneWithoutErrors_BattleScene()
        {
            EditorAccess.instance.SceneLoader.LoadBattleScene(false);
            yield return null;
        }
    }
}

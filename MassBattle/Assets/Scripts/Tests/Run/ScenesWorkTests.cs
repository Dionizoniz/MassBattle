using System.Collections;
using MassBattle.Logic.Utilities;
using MassBattle.Tests.Editor;
using UnityEngine.TestTools;

namespace MassBattle.Tests.Run
{
    public class ScenesWorkTests
    {
        [UnityTest]
        public IEnumerator _00_RunSceneWithoutErrors_LaunchMenuScene()
        {
            EditorAccess.Instance.SceneLoader.LoadLaunchMenuScene(false);
            yield return null;
        }

        [UnityTest]
        public IEnumerator _01_RunSceneWithoutErrors_BattleScene()
        {
            EditorAccess.Instance.SceneLoader.LoadBattleScene(false);
            yield return null;
        }
    }
}

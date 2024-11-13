using System.Collections.Generic;
using System.Linq;
using MassBattle.Core.Entities.Tests;
using MassBattle.Logic.BattleCreator;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Editor
{
    public class ProjectSetupTests : BaseTests
    {
        [Test]
        public void _00_TestSetup_BattleSetup()
        {
            List<BattleSetup> battleSetups = FindAssets<BattleSetup>();
            Assert.True(IsCorrectAssetsSetup(battleSetups));
        }

        private bool IsCorrectAssetsSetup<T>(List<T> assets) where T : ICheckSetup
        {
            bool isCorrectSetup = true;
            bool isAnySetup = assets.Any();

            foreach (T asset in assets)
            {
                isCorrectSetup &= asset.IsSetupCorrect();
            }

            return isCorrectSetup && isAnySetup;
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        // [UnityTest]
        // public IEnumerator NewTestScriptWithEnumeratorPasses()
        // {
        //     // Use the Assert class to test conditions.
        //     // Use yield to skip a frame.
        //     yield return null;
        // }
    }
}

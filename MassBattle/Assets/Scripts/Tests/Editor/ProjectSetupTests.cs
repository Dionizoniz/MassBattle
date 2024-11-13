using System.Collections.Generic;
using System.Linq;
using MassBattle.Core.Entities.Database;
using MassBattle.Core.Entities.Engine;
using MassBattle.Core.Entities.Installers;
using MassBattle.Core.Entities.MVC;
using MassBattle.Core.Entities.Tests;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.BattleCreator;
using NUnit.Framework;

namespace Tests.Editor
{
    public class ProjectSetupTests : BaseTests
    {
        [Test]
        public void _00_TestSetup_BattleSetups()
        {
            List<BattleSetup> assets = FindAssets<BattleSetup>();
            Assert.True(IsCorrectAssetsSetup(assets));
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

        [Test]
        public void _01_TestSetup_Installers()
        {
            List<BaseInstaller> assets = FindAssets<BaseInstaller>();
            Assert.True(IsCorrectAssetsSetup(assets));
        }

        [Test]
        public void _02_TestSetup_Databases()
        {
            List<BaseDatabase> assets = FindAssets<BaseDatabase>();
            Assert.True(IsCorrectAssetsSetup(assets));
        }

        [Test]
        public void _03_TestSetup_SceneLoaders()
        {
            List<SceneLoader> assets = FindAssets<SceneLoader>();
            Assert.True(IsCorrectAssetsSetup(assets));
        }

        [Test]
        public void _04_TestSetup_BattleSpawners()
        {
            List<BattleSpawner> assets = FindAssets<BattleSpawner>();
            Assert.True(IsCorrectAssetsSetup(assets));
        }

        [Test]
        public void _05_TestSetup_BattleSpawnersHaveEnoughSpawnAreas()
        {
            List<BattleSpawner> spawners = FindAssets<BattleSpawner>();
            List<BattleSetup> setups = FindAssets<BattleSetup>();

            int minSpawnArmyBoundsCount = spawners.Min(spawner => spawner.SpawnArmyBoundsCount);
            int maxArmyIdsCount = setups.Max(setup => setup.FindAllArmySetupIds().Count);

            Assert.True(minSpawnArmyBoundsCount >= maxArmyIdsCount);
        }

        [Test]
        public void _06_TestSetup_SceneEntities()
        {
            List<BaseSceneEntity> assets = FindAssets<BaseSceneEntity>();
            Assert.True(IsCorrectAssetsSetup(assets));
        }

        [Test]
        public void _07_TestSetup_ControllersInMVC()
        {
            List<BaseController> assets = FindAssets<BaseController>();
            Assert.True(IsCorrectAssetsSetup(assets));
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

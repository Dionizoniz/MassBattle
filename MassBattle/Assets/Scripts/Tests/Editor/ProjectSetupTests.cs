using System.Collections.Generic;
using System.Linq;
using MassBattle.Core.Databases;
using MassBattle.Core.Engine;
using MassBattle.Core.Entities;
using MassBattle.Core.Installers;
using MassBattle.Core.Patterns.MVC;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.BattleCreator;
using MassBattle.Logic.Databases.ArmyDatabase;
using NUnit.Framework;
using UnityEditor;

namespace MassBattle.Tests.Editor
{
    public class ProjectSetupTests : BaseTests
    {
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
        public void _00_TestSetup_Installers()
        {
            List<BaseInstaller> assets = FindAssets<BaseInstaller>();
            Assert.True(IsCorrectAssetsSetup(assets));
        }

        [Test]
        public void _01_TestSetup_Databases()
        {
            List<BaseDatabase> assets = FindAssets<BaseDatabase>();
            Assert.True(IsCorrectAssetsSetup(assets));
        }

        [Test]
        public void _02_TestSetup_SceneLoaders()
        {
            List<SceneLoader> assets = FindAssets<SceneLoader>();
            Assert.True(IsCorrectAssetsSetup(assets));
        }

        [Test]
        public void _03_TestSetup_BattleSpawners()
        {
            List<BattleSpawner> assets = FindAssets<BattleSpawner>();
            Assert.True(IsCorrectAssetsSetup(assets));
        }

        [Test]
        public void _04_TestSetup_BattleSpawnersHaveEnoughSpawnAreas()
        {
            List<BattleSpawner> spawners = FindAssets<BattleSpawner>();
            List<ArmyDatabase> databases = FindAssets<ArmyDatabase>();

            int minSpawnArmyBoundsCount = spawners.Min(spawner => spawner.SpawnArmyBoundsCount);
            int maxArmyIdsCount = databases.Max(setup => setup.FindAllDescriptorIds().Count());

            Assert.True(minSpawnArmyBoundsCount >= maxArmyIdsCount);
        }

        [Test]
        public void _05_TestSetup_SceneEntities()
        {
            List<BaseSceneEntity> assets = FindAssets<BaseSceneEntity>();
            Assert.True(IsCorrectAssetsSetup(assets));
        }

        [Test]
        public void _06_TestSetup_ControllersInMVC()
        {
            List<BaseController> assets = FindAssets<BaseController>();
            Assert.True(IsCorrectAssetsSetup(assets));
        }

        [Test]
        public void _07_TestSetup_ScenesInBuildSettings()
        {
            EditorBuildSettingsScene[] editorBuildSettingsScenes = EditorBuildSettings.scenes;
            bool isNotEmptySceneCollection = editorBuildSettingsScenes.All(s => s.enabled);

            Assert.True(isNotEmptySceneCollection);
        }
    }
}

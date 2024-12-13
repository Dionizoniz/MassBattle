using System.Collections.Generic;
using System.Linq;
using MassBattle.Core.Databases;
using MassBattle.Core.Engine;
using MassBattle.Core.Entities;
using MassBattle.Core.Installers;
using MassBattle.Core.Patterns.MVC;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.Battle.Setup;
using MassBattle.Logic.Battle.Spawner;
using NUnit.Framework;
using UnityEditor;
using Zenject;

namespace MassBattle.Tests.Editor
{
    public class ProjectSetupTests : BaseTests
    {
        [Test]
        public void _00_TestSetup_Installers()
        {
            IEnumerable<MonoInstaller> monoInstallers = FindAssets<SceneContext>().SelectMany(s => s.Installers);
            List<ExtendedMonoInstaller> extendedMonoInstallers = new();
            ValidationData validationData = new();

            foreach (MonoInstaller installer in monoInstallers)
            {
                if (installer is ExtendedMonoInstaller extendedMonoInstaller)
                {
                    extendedMonoInstallers.Add(extendedMonoInstaller);
                }
                else
                {
                    validationData.AddErrorMessage($"Installer {installer.GetType()} has wrong base class.");
                }
            }

            validationData = IsCorrectAssetsSetup(extendedMonoInstallers, validationData);

            Assert.True(validationData.IsValid, validationData.ErrorMessage);
        }

        private ValidationData IsCorrectAssetsSetup<T>(List<T> assets, ValidationData data = null) where T : ICheckSetup
        {
            data ??= new ValidationData();

            if (assets.Any() == false)
            {
                data.AddErrorMessage($"No assets of type {typeof(T)} found.");
            }

            foreach (T asset in assets)
            {
                if (asset.IsSetupCorrect() == false)
                {
                    data.AddErrorMessage($"Asset {asset} is not setup correctly");
                }
            }

            return data;
        }

        [Test]
        public void _01_TestSetup_Databases()
        {
            List<BaseDatabase> assets = FindAssets<BaseDatabase>();
            ValidationData validationData = IsCorrectAssetsSetup(assets);

            Assert.True(validationData.IsValid, validationData.ErrorMessage);
        }

        [Test]
        public void _02_TestSetup_SceneLoaders()
        {
            List<SceneLoader> assets = FindAssets<SceneLoader>();
            ValidationData validationData = IsCorrectAssetsSetup(assets);

            Assert.True(validationData.IsValid, validationData.ErrorMessage);
        }

        [Test]
        public void _03_TestSetup_BattleSpawners()
        {
            List<ArmySpawner> assets = FindAssets<ArmySpawner>();
            ValidationData validationData = IsCorrectAssetsSetup(assets);

            Assert.True(validationData.IsValid, validationData.ErrorMessage);
        }

        [Test]
        public void _04_TestSetup_BattleSpawnersHaveEnoughSpawnAreas()
        {
            List<ArmySpawner> spawners = FindAssets<ArmySpawner>();
            List<BattleSetup> setups = FindAssets<BattleSetup>();

            int minSpawnArmyBoundsCount = spawners.Min(spawner => spawner.SpawnArmyBoundsCount);
            int maxArmyIdsCount = setups.Max(setup => setup.SavedArmiesData.Count);

            Assert.True(minSpawnArmyBoundsCount >= maxArmyIdsCount);
        }

        [Test]
        public void _05_TestSetup_SceneEntities()
        {
            List<BaseSceneEntity> assets = FindAssets<BaseSceneEntity>();
            ValidationData validationData = IsCorrectAssetsSetup(assets);

            Assert.True(validationData.IsValid, validationData.ErrorMessage);
        }

        [Test]
        public void _06_TestSetup_ControllersInMVC()
        {
            List<BaseController> assets = FindAssets<BaseController>();
            ValidationData validationData = IsCorrectAssetsSetup(assets);

            Assert.True(validationData.IsValid, validationData.ErrorMessage);
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

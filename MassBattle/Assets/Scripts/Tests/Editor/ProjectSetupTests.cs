using System.Collections.Generic;
using System.Linq;
using MassBattle.Core.Databases;
using MassBattle.Core.Engine;
using MassBattle.Core.Installers;
using MassBattle.Core.Patterns.MVC;
using MassBattle.Core.SceneLoaders;
using MassBattle.Logic.Battle.Setup;
using MassBattle.Logic.Battle.Spawner;
using MassBattle.Logic.Databases.Colors;
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

        [Test]
        public void _01_TestSetup_Databases()
        {
            ValidateAssets<BaseDatabase>();
        }

        [Test]
        public void _02_TestSetup_SceneLoaders()
        {
            ValidateAssets<SceneLoader>();
        }

        [Test]
        public void _03_TestSetup_BattleSpawners()
        {
            ValidateAssets<ArmySpawner>();
        }

        [Test]
        public void _04_TestSetup_BattleSpawnersHaveEnoughSpawnAreas()
        {
            List<ArmySpawner> spawners = FindAssets<ArmySpawner>();
            List<BattleSetup> setups = FindAssets<BattleSetup>();

            int minBoundsCount = spawners.Min(spawner => spawner.SpawnArmyBoundsCount);
            int maxArmiesCount = setups.Max(setup => setup.ArmiesData.Count);

            Assert.True(minBoundsCount >= maxArmiesCount);
        }

        [Test]
        public void _05_TestSetup_SceneEntities()
        {
            ValidateAssets<BaseSceneEntity>();
        }

        [Test]
        public void _06_TestSetup_ControllersInMvc()
        {
            ValidateAssets<BaseController>();
        }

        [Test]
        public void _07_TestSetup_ScenesInBuildSettings()
        {
            EditorBuildSettingsScene[] editorBuildSettingsScenes = EditorBuildSettings.scenes;
            bool isNotEmptySceneCollection = editorBuildSettingsScenes.All(s => s.enabled);

            Assert.True(isNotEmptySceneCollection);
        }

        [Test]
        public void _08_TestSetup_ColorDatabaseHaveEnoughColors()
        {
            List<ColorDatabase> databases = FindAssets<ColorDatabase>();
            List<BattleSetup> setups = FindAssets<BattleSetup>();

            int minColorsCount = databases.Min(spawner => spawner.AllColors.Count);
            int maxArmiesCount = setups.Max(setup => setup.ArmiesData.Count);

            Assert.True(minColorsCount >= maxArmiesCount);
        }

        [Test]
        public void _09_TestSetup_BattleSetups()
        {
            ValidateAssets<BattleSetup>();
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using MassBattle.Core.Entities;
using UnityEditor;
using UnityEngine;
using NUnit.Framework;

namespace MassBattle.Tests.Editor
{
    public class BaseTests
    {
        protected List<T> FindAssets<T>(string filter = "*") where T : Object
        {
            List<T> assetsList = new();

            var assetPaths = FindAssetPaths(filter);

            var assets = assetPaths.Select(AssetDatabase.LoadAssetAtPath<T>).Where(a => a != null);
            assetsList.AddRange(assets);

            return assetsList;
        }

        private IEnumerable<string> FindAssetPaths(string filter = "*")
        {
            var guids = AssetDatabase.FindAssets($"{filter}");
            return guids.Select(AssetDatabase.GUIDToAssetPath);
        }

        protected void ValidateAssets<T>() where T : Object, ICheckSetup
        {
            List<T> assets = FindAssets<T>();
            ValidationData validationData = IsCorrectAssetsSetup(assets);

            Assert.True(validationData.IsValid, validationData.ErrorMessage);
        }

        protected ValidationData IsCorrectAssetsSetup<T>(List<T> assets, ValidationData data = null)
                where T : ICheckSetup
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
    }
}

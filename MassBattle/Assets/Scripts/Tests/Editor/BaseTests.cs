using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MassBattle.Tests.Editor
{
    public class BaseTests
    {
        public List<T> FindAssets<T>(string filter = "*") where T : Object
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
    }
}

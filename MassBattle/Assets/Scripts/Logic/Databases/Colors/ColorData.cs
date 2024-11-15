using System;
using MassBattle.Core.Entities.Database;
using MassBattle.Core.Utilities;
using UnityEngine;

namespace MassBattle.Logic.Databases.Colors
{
    [Serializable]
    public class ColorData : IId
    {
        [SerializeField, ReadOnly]
        private string _id;
        [SerializeField]
        private Color color = Color.white;

        public string Id => _id;
        public Color Color => color;

        public void GenerateId(int index)
        {
            _id = $"{nameof(ColorData)}{index}";
        }
    }
}

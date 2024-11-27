using System;
using MassBattle.Core.Databases;
using UnityEngine;

namespace MassBattle.Logic.Databases.Colors
{
    [Serializable]
    public class ColorData : BaseData
    {
        [SerializeField]
        private Color color = Color.white;

        public Color Color => color;

        protected override string ClassName => nameof(ColorData);
    }
}

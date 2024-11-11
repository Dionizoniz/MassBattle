using System;
using MassBattle.Core.Entities.Database;
using UnityEngine;

namespace MassBattle.Logic.Databases
{
    [Serializable]
    public class ColorData : IId
    {
        [SerializeField]
        private string _id;
        [SerializeField]
        private Color color = Color.white;

        public string Id => _id;
        public Color Color => color;
    }
}

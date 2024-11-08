using System;
using MassBattle.Core.Entities;
using UnityEngine;

namespace MassBattle.Logic.Databases
{
    [Serializable]
    public class ColorData : IId
    {
        [SerializeField]
        private string id;
        [SerializeField]
        private Color color = Color.white;

        public string Id { get; }
        public Color Color => color;
    }
}

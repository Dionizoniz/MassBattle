using MassBattle.Core.Entities;
using UnityEngine;

namespace MassBattle.Logic.Databases
{
    public interface IColorDatabase : IBaseDatabase<ColorData>
    {
        int FindColorIndex(Color color);
    }
}

using MassBattle.Core.Entities;
using MassBattle.Core.Entities.Database;
using UnityEngine;

namespace MassBattle.Logic.Databases
{
    public interface IColorDatabase : IBaseDatabase<ColorData>
    {
        int FindColorIndex(Color color);
    }
}

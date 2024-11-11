using MassBattle.Logic.Units.Weapons;

namespace MassBattle.Logic.Providers
{
    public interface IUnitsFactory
    {
        Arrow CreateArrowInstance(Arrow arrowPrefab);
        void ReturnArrowInstance(Arrow arrow);
    }
}

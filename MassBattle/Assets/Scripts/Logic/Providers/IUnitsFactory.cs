using MassBattle.Logic.Units.Weapons;

namespace MassBattle.Logic.Providers
{
    public interface IUnitsFactory
    {
        void Initialize();
        Arrow CreateArrowInstance(Arrow arrowPrefab);
        void ReturnArrowInstance(Arrow arrow);
    }
}

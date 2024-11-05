namespace MassBattle.Logic.Armies
{
    public class ArmyProvider : IArmyProvider
    {
        public Army Army1 { get; } = new();
        public Army Army2 { get; } = new();
    }
}

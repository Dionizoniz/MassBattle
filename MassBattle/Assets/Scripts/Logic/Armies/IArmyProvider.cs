namespace MassBattle.Logic.Armies
{
    public interface IArmyProvider
    {
        void ClearArmies();
        void RegisterArmy(Army army);
        Army FindArmyBy(string armyId);
        void FillUpEnemiesForRegisteredArmies();
    }
}

using System.Collections.Generic;

namespace MassBattle.Logic.Setup
{
    public interface IBattleSetup
    {
        void RegisterArmySetup(ArmySetup armySetup);
        List<string> FindAllArmySetupIds();
        ArmySetup TryFindArmySetupBy(string id);
        void ClearRegisteredArmySetups();
    }
}

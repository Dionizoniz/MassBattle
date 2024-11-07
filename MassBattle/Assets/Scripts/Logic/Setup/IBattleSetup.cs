using System.Collections.Generic;

namespace MassBattle.Logic.Setup
{
    public interface IBattleSetup
    {
        void SaveArmySetup(ArmySetup armySetup);
        List<string> FindAllArmySetupIds();
        ArmySetup TryFindArmySetupBy(string id);
        void ClearSavedArmySetups();
    }
}

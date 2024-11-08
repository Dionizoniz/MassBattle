using MassBattle.Logic.Databases;
using MassBattle.Logic.Setup;

namespace MassBattle.UI.LaunchMenu
{
    public interface ILaunchMenuController
    {
        void InjectData(BattleSetup battleSetup, ColorDatabase colorDatabase);
        void StartBattle();
    }
}

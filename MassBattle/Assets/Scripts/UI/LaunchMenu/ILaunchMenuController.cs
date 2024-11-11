using MassBattle.Logic.Databases;
using MassBattle.Logic.Setup;

namespace MassBattle.UI.LaunchMenu
{
    public interface ILaunchMenuController
    {
        void InjectData(IBattleSetup battleSetup, ColorDatabase colorDatabase);
        void StartBattle();
    }
}

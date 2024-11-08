using MassBattle.Logic.Setup;

namespace MassBattle.UI.LaunchMenu
{
    public interface ILaunchMenuController
    {
        void InjectData(BattleSetup battleSetup);
        void StartBattle();
    }
}

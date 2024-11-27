using System.Collections.Generic;
using MassBattle.Core.Patterns.MVC;
using MassBattle.Logic.Databases.ArmyDatabase;

namespace MassBattle.UI.LaunchMenu
{
    public class ArmyPanelModel : Model<ArmyPanelView>
    {
        public InitialArmyData CreateArmySetup()
        {
            Dictionary<string, int> unitsCountSetup = _view.FindUnitsCountSetup();

            return new InitialArmyData(_view.ArmyId, _view.ArmyName, unitsCountSetup, _view.StrategyType,
                                       _view.ArmyColor, _view.IsArmyActive);
        }
    }
}

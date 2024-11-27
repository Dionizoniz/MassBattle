using MassBattle.Core.Patterns.MVC;
using MassBattle.Logic.Databases.ArmyDatabase;
using MassBattle.Logic.Databases.Colors;
using MassBattle.Logic.Databases.UnitDatabase;
using UnityEngine;

namespace MassBattle.UI.LaunchMenu
{
    public class ArmyPanelController : Controller<ArmyPanelModel, ArmyPanelView>
    {
        public bool IsArmyActive => _view.IsArmyActive;
        public string ArmyName => _view.ArmyName;
        public Color ArmyColor => _view.ArmyColor;

        public void InitializeData(
                InitialArmyData initialArmyData, IColorDatabase colorDatabase, IUnitDatabase unitDatabase,
                IArmyDatabase armyDatabase)
        {
            _view.InitializeData(initialArmyData, colorDatabase, unitDatabase, armyDatabase);
        }

        public InitialArmyData CreateArmySetup()
        {
            return _model.CreateArmySetup();
        }

        public void ChangeArmyColorToNext()
        {
            _view.ChangeArmyColorToNext();
        }
    }
}

using MassBattle.Core.Patterns.MVC;
using MassBattle.Logic.Databases.Armies;
using UnityEngine;

namespace MassBattle.UI.LaunchMenu
{
    public class ArmyPanelController : Controller<ArmyPanelModel, ArmyPanelView>
    {
        public bool IsArmyActive => _view.IsArmyActive;
        public string ArmyName => _view.ArmyName;
        public Color ArmyColor => _view.ArmyColor;

        public void InitializeData(InitialArmyData initialArmyData)
        {
            _view.InitializeData(initialArmyData);
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

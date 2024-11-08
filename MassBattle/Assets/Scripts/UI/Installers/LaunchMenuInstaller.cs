using MassBattle.Logic.Setup;
using MassBattle.UI.LaunchMenu;
using UnityEngine;

namespace MassBattle.UI.Installers
{
    public class LaunchMenuInstaller : MonoBehaviour
    {
        [SerializeField]
        private LaunchMenuController _launchMenuController;
        [SerializeField]
        private BattleSetup _battleSetup;

        private void Awake()
        { }
    }
}

using MassBattle.Core.Entities.Engine;
using MassBattle.Logic.Installers;
using MassBattle.UI.EndBattlePanel;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MassBattle.UI.Installers
{
    public class BattleUiInstaller : ExtendedMonoBehaviour, IBattleUiInstaller
    {
        [SerializeField]
        private BattleInstaller _spawnedBattleInstaller;

        [SerializeField]
        private EndBattlePanelController _endBattlePanelControllerToSpawn;
        [SerializeField]
        private EventSystem _eventSystemToSpawn;

        private void Awake()
        {
            SpawnEndBattlePanelController();
            SpawnEventSystem();
        }

        private void SpawnEndBattlePanelController()
        {
            IEndBattlePanelController endBattlePanel = Instantiate(_endBattlePanelControllerToSpawn);
            endBattlePanel.InjectData(_spawnedBattleInstaller.ArmyProvider);
        }

        private void SpawnEventSystem()
        {
            Instantiate(_eventSystemToSpawn);
        }
    }
}

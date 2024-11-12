using System;
using MassBattle.Core.Entities.Engine;
using MassBattle.Core.SceneLoaders;
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
        private SceneLoader _sceneLoader;

        [SerializeField]
        private EndBattlePanelController _endBattlePanelControllerToSpawn;
        [SerializeField]
        private EventSystem _eventSystemToSpawn;

        private IEndBattlePanelController _endBattlePanel;

        private void Awake()
        {
            SpawnEndBattlePanelController();
            SpawnEventSystem();
        }

        private void SpawnEndBattlePanelController()
        {
            _endBattlePanel = Instantiate(_endBattlePanelControllerToSpawn);
        }

        private void SpawnEventSystem()
        {
            Instantiate(_eventSystemToSpawn);
        }

        private void Start()
        {
            InjectData();
        }

        private void InjectData()
        {
            _endBattlePanel.InjectData(_spawnedBattleInstaller.ArmyProvider, _sceneLoader);
        }
    }
}

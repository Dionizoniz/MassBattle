using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MassBattle.Logic.Setup
{
    [CreateAssetMenu(menuName = "Create BattleSetup", fileName = "BattleSetup", order = 0)]
    public class BattleSetup : ScriptableObject, IBattleSetup
    {
        [SerializeField]
        private List<ArmySetup> _defaultArmySetups = new();

        // Add readonly if you do not need save data between runs in editor mode
        private List<ArmySetup> _savedArmySetups = new();

        private bool UseSavedArmySetups => _savedArmySetups != null && _savedArmySetups.Count > 0;
        private IEnumerable<ArmySetup> ArmySetups => UseSavedArmySetups ? _savedArmySetups : _defaultArmySetups;

        public void SaveArmySetup(ArmySetup armySetup)
        {
            int index = FindSavedArmySetupIndexBy(armySetup.ArmyId);

            if (index > 0)
            {
                _savedArmySetups.RemoveAt(index);
            }

            _savedArmySetups.Add(armySetup);
        }

        private int FindSavedArmySetupIndexBy(string id)
        {
            return _savedArmySetups.FindIndex(armySetup => armySetup.ArmyId == id);
        }

        public List<string> FindAllArmySetupIds()
        {
            return ArmySetups.Select(x => x.ArmyId).ToList();
        }

        public ArmySetup TryFindArmySetupBy(string id)
        {
            return ArmySetups.FirstOrDefault(armySetup => armySetup.ArmyId == id);
        }

        public void ClearSavedArmySetups()
        {
            _savedArmySetups.Clear();
        }
    }
}

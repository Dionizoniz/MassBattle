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

        private readonly List<ArmySetup> _registeredArmySetups = new();

        private bool ExistRegisteredArmySetups => _registeredArmySetups != null && _registeredArmySetups.Count > 0;
        private List<ArmySetup> ArmySetups => ExistRegisteredArmySetups ? _registeredArmySetups : _defaultArmySetups;

        public void RegisterArmySetup(ArmySetup armySetup)
        {
            int index = FindRegisteredArmySetupIndexBy(armySetup.ArmyId);

            if (index > 0)
            {
                _registeredArmySetups.RemoveAt(index);
            }

            _registeredArmySetups.Add(armySetup);
        }

        private int FindRegisteredArmySetupIndexBy(string id)
        {
            return _registeredArmySetups.FindIndex(armySetup => armySetup.ArmyId == id);
        }

        public List<string> FindAllArmySetupIds()
        {
            return ArmySetups.Select(x => x.ArmyId).ToList();
        }

        public ArmySetup TryFindArmySetupBy(string id)
        {
            return ArmySetups.FirstOrDefault(armySetup => armySetup.ArmyId == id);
        }

        public void ClearRegisteredArmySetups()
        {
            _registeredArmySetups.Clear();
        }
    }
}

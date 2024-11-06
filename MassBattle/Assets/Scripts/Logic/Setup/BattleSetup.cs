using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MassBattle.Logic.Setup
{
    [CreateAssetMenu(menuName = "Create BattleSetup", fileName = "BattleSetup", order = 0)]
    public class BattleSetup : ScriptableObject
    {
        [SerializeField]
        private List<ArmySetup> defaultArmySetups = new();

        private List<ArmySetup> registeredArmySetups = new();

        private bool ExistRegisteredArmySetups => registeredArmySetups != null && registeredArmySetups.Count > 0;
        private List<ArmySetup> ArmySetups => ExistRegisteredArmySetups ? registeredArmySetups : defaultArmySetups;

        public void RegisterArmySetup(ArmySetup armySetup)
        {
            int index = FindRegisteredArmySetupIndexBy(armySetup.Id);

            if (index > 0)
            {
                registeredArmySetups.RemoveAt(index);
            }

            registeredArmySetups.Add(armySetup);
        }

        private int FindRegisteredArmySetupIndexBy(string id)
        {
            return registeredArmySetups.FindIndex(armySetup => armySetup.Id == id);
        }

        public ArmySetup TryFindArmySetupBy(string id)
        {
            return ArmySetups.FirstOrDefault(armySetup => armySetup.Id == id);
        }

        public void ClearRegisteredArmySetups()
        {
            registeredArmySetups.Clear();
        }
    }
}

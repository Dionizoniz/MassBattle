// using System;
// using System.Collections.Generic;
// using System.Linq;
// using MassBattle.Core.Entities.Engine;
// using MassBattle.Core.Entities.Tests;
// using MassBattle.Logic.Armies;
// using UnityEngine;
//
// namespace MassBattle.Logic.BattleCreator
// {
//     [CreateAssetMenu(menuName = nameof(BattleSetup), fileName = nameof(BattleSetup), order = 0)]
//     public class BattleSetup : ExtendedScriptableObject, IBattleSetup, ICheckSetup
//     {
//         private const int MIN_ARMIES_COUNT = 2;
//         private const int MAX_ARMIES_COUNT = 4;
//
//         [SerializeField]
//         private List<ArmySetup> _defaultArmySetups = new();
//
//         // Add readonly if you do not need save data between runs in editor mode
//         private List<ArmySetup> _savedArmySetups = new();
//
//         private bool UseSavedArmySetups => _savedArmySetups != null && _savedArmySetups.Count > 0;
//         private IEnumerable<ArmySetup> ArmySetups => UseSavedArmySetups ? _savedArmySetups : _defaultArmySetups;
//
//         public void SaveArmySetup(ArmySetup armySetup)
//         {
//             int index = FindSavedArmySetupIndexBy(armySetup.Id);
//
//             if (index >= 0)
//             {
//                 _savedArmySetups.RemoveAt(index);
//             }
//
//             _savedArmySetups.Add(armySetup);
//         }
//
//         private int FindSavedArmySetupIndexBy(string id)
//         {
//             return _savedArmySetups.FindIndex(armySetup => armySetup.Id == id);
//         }
//
//         public List<string> FindAllArmySetupIds()
//         {
//             return ArmySetups.Select(x => x.Id).ToList();
//         }
//
//         public ArmySetup TryFindArmySetupBy(string id)
//         {
//             return ArmySetups.FirstOrDefault(armySetup => armySetup.Id == id);
//         }
//
//         public void ClearSavedArmySetups()
//         {
//             _savedArmySetups.Clear();
//         }
//
//         public bool IsSetupCorrect()
//         {
//             bool isSetupCorrect = true;
//
//             int defaultArmiesCount = _defaultArmySetups.Count;
//             isSetupCorrect &= defaultArmiesCount >= MIN_ARMIES_COUNT && defaultArmiesCount <= MAX_ARMIES_COUNT;
//
//             foreach (var armySetup in _defaultArmySetups)
//             {
//                 isSetupCorrect &= armySetup.IsSetupCorrect();
//             }
//
//             return isSetupCorrect;
//         }
//
//         private void OnValidate()
//         {
//             for (int i = 0; i < _defaultArmySetups.Count; i++)
//             {
//                 _defaultArmySetups[i].GenerateId(i);
//             }
//         }
//     }
// }


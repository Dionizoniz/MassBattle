using System.Collections.Generic;
using MassBattle.Core.Entities.MVC;
using TMPro;
using UnityEngine;

namespace MassBattle.UI.LoadingScenePanel
{
    public class LoadingScreenPanelView : View
    {
        [SerializeField]
        private TextMeshProUGUI _hintLabel;

        [SerializeField]
        private List<string> _hintMessages = new();

        private void Awake()
        {
            ShowRandomHint();
        }

        private void ShowRandomHint()
        {
            int randomIndex = Random.Range(0, _hintMessages.Count);
            _hintLabel.text = _hintMessages[randomIndex];
        }
    }
}

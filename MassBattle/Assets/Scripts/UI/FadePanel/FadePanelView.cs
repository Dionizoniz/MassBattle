using MassBattle.Core.Entities.MVC;
using UnityEngine;

namespace MassBattle.UI.FadePanel
{
    public class FadePanelView : View
    {
        private const string ANIMATION_CURVES_TOOLTIP_MESSAGE = "X - Time in seconds, Y - alpha value.";

        [SerializeField]
        private CanvasGroup _fadeAlphaController;

        [SerializeField, Tooltip(ANIMATION_CURVES_TOOLTIP_MESSAGE)]
        private AnimationCurve _fadeInSetup = AnimationCurve.Linear(0f, 0f, 0.5f, 1f);
        [SerializeField, Tooltip(ANIMATION_CURVES_TOOLTIP_MESSAGE)]
        private AnimationCurve _fadeOutSetup = AnimationCurve.Linear(0f, 1f, 0.5f, 0f);

        public void FadeIn(bool isInstant)
        {
            // TODO use setup
            _fadeAlphaController.alpha = 1;
        }

        public void FadeOut(bool isInstant)
        {
            // TODO use setup
            _fadeAlphaController.alpha = 0;
        }
    }
}

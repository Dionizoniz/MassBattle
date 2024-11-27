using System.Collections;
using MassBattle.Core.Patterns.MVC;
using UnityEngine;

namespace MassBattle.UI.FadePanel
{
    public class FadePanelView : View
    {
        private const string ANIMATION_CURVES_TOOLTIP_MESSAGE = "X - Time in seconds, Y - alpha value.";
        private const float MIN_ALPHA_VALUE = 0f;
        private const float MAX_ALPHA_VALUE = 1f;

        [SerializeField]
        private CanvasGroup _fadeAlphaController;

        [SerializeField, Tooltip(ANIMATION_CURVES_TOOLTIP_MESSAGE)]
        private AnimationCurve _fadeInSetup = AnimationCurve.Linear(0f, 0f, 0.5f, 1f);
        [SerializeField, Tooltip(ANIMATION_CURVES_TOOLTIP_MESSAGE)]
        private AnimationCurve _fadeOutSetup = AnimationCurve.Linear(0f, 1f, 0.5f, 0f);

        private Coroutine _cachedFadingCoroutine;

        public void FadeIn(bool isInstant)
        {
            if (isInstant)
            {
                ChangeFadeAlphaValue(MAX_ALPHA_VALUE);
            }
            else
            {
                StartFadingProcess(_fadeInSetup);
            }
        }

        private void ChangeFadeAlphaValue(float alpha)
        {
            _fadeAlphaController.alpha = alpha;
            _fadeAlphaController.blocksRaycasts = alpha >= MAX_ALPHA_VALUE;
        }

        private void StartFadingProcess(AnimationCurve fadeSetup)
        {
            StopFadingProcess();

            _cachedFadingCoroutine = StartCoroutine(FadingProcess(fadeSetup));
        }

        private void StopFadingProcess()
        {
            if (_cachedFadingCoroutine != null)
            {
                StopCoroutine(_cachedFadingCoroutine);
            }
        }

        private IEnumerator FadingProcess(AnimationCurve fadeSetup)
        {
            float time = 0f;

            while (true)
            {
                float alphaValue = fadeSetup.Evaluate(time);
                ChangeFadeAlphaValue(alphaValue);

                time += Time.deltaTime;
                yield return null;
            }
        }

        public void FadeOut(bool isInstant)
        {
            if (isInstant)
            {
                ChangeFadeAlphaValue(MIN_ALPHA_VALUE);
            }
            else
            {
                StartFadingProcess(_fadeOutSetup);
            }
        }

        private void OnDestroy()
        {
            StopFadingProcess();
        }
    }
}

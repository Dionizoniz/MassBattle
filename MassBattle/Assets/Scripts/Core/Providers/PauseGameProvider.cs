using UnityEngine;

namespace MassBattle.Core.Providers
{
    public class PauseGameProvider : IPauseGameProvider
    {
        public void PauseGame()
        {
            ChangeTimeScale(0f);
        }

        private void ChangeTimeScale(float timeScale)
        {
            Time.timeScale = timeScale;
        }

        public void ResumeGame()
        {
            ChangeTimeScale(1f);
        }
    }
}

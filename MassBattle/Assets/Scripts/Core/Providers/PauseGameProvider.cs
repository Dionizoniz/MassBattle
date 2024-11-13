using UnityEngine;

namespace MassBattle.Core.Providers
{
    public class PauseGameProvider : IPauseGameProvider
    {
        public void PauseGame()
        {
            Time.timeScale = 0f;
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
        }
    }
}

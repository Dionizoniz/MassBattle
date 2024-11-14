using UnityEngine;

namespace MassBattle.Core.Providers
{
    public class ExitGameProvider : IExitGameProvider
    {
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}

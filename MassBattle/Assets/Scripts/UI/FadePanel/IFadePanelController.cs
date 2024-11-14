namespace MassBattle.UI.FadePanel
{
    public interface IFadePanelController
    {
        void FadeIn(bool isInstant = false);
        void FadeOut(bool isInstant = false);
    }
}

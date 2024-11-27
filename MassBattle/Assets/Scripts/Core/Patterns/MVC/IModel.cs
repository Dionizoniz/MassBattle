namespace MassBattle.Core.Patterns.MVC
{
    public interface IModel<TView> where TView : IView
    {
        void Initialize(TView view);
    }
}

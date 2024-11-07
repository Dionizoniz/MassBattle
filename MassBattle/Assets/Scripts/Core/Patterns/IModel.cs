namespace MassBattle.Core.Patterns
{
    public interface IModel<TView> where TView : IView
    {
        void Initialize(TView view);
    }
}

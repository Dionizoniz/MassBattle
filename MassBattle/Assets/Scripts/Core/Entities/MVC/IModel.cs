namespace MassBattle.Core.Entities.MVC
{
    public interface IModel<TView> where TView : IView
    {
        void Initialize(TView view);
    }
}

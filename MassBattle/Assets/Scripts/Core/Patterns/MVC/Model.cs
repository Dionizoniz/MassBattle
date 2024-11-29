using MassBattle.Core.Engine;

namespace MassBattle.Core.Patterns.MVC
{
    public abstract class Model<TView> : ExtendedMonoBehaviour, IModel<TView> where TView : IView
    {
        protected TView _view;

        public void Initialize(TView view)
        {
            _view = view;
        }
    }
}

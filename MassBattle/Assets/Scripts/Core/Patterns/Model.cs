using UnityEngine;

namespace MassBattle.Core.Patterns
{
    public abstract class Model<TView> : MonoBehaviour, IModel<TView> where TView : IView
    {
        protected TView _view;

        public void Initialize(TView view)
        {
            _view = view;
        }
    }
}

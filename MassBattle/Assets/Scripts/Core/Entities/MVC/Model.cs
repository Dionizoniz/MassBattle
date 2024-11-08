using UnityEngine;

namespace MassBattle.Core.Entities.MVC
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

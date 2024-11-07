using UnityEngine;

namespace MassBattle.Core.Patterns
{
    public abstract class Controller<TModel, TView> : MonoBehaviour, IController
            where TModel : IModel<TView>
            where TView : IView
    {
        [SerializeField]
        protected TModel _model;

        [SerializeField]
        protected TView _view;

        private void Awake()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            _model.Initialize(_view);
        }
    }
}

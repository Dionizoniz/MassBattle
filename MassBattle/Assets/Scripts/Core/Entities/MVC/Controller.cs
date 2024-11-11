using MassBattle.Core.Entities.Engine;
using UnityEngine;

namespace MassBattle.Core.Entities.MVC
{
    public abstract class Controller<TModel, TView> : ExtendedMonoBehaviour, IController
            where TModel : IModel<TView>
            where TView : IView
    {
        [SerializeField]
        protected TModel _model;

        [SerializeField]
        protected TView _view;

        private void Start()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            _model.Initialize(_view);
        }
    }
}

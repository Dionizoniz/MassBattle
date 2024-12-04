using MassBattle.Core.Entities;
using UnityEngine;
using Zenject;

namespace MassBattle.Core.Installers
{
    public abstract class ExtendedMonoInstaller : MonoInstaller, ICheckSetup
    {
        protected void Bind<T>()
        {
            Container.Bind<T>().AsSingle().NonLazy();
        }

        protected void BindInterfacesTo<T>()
        {
            Container.BindInterfacesTo<T>().AsSingle().NonLazy();
        }

        protected void BindInterfacesAndSelfTo<T>()
        {
            Container.BindInterfacesAndSelfTo<T>().AsSingle().NonLazy();
        }

        protected void BindFromComponentInNewPrefab<T>(T prefab) where T : Component
        {
            Container.Bind<T>().FromComponentInNewPrefab(prefab).AsSingle().NonLazy();
        }

        protected void BindInterfacesToFromInstance<T>(T instance)
        {
            Container.BindInterfacesTo<T>().FromInstance(instance).AsSingle().NonLazy();
        }

        protected void BindInterfacesToFromComponentInNewPrefab<T>(T prefab) where T : Component
        {
            Container.BindInterfacesTo<T>().FromComponentInNewPrefab(prefab).AsSingle().NonLazy();
        }

        public abstract bool IsSetupCorrect();
    }
}

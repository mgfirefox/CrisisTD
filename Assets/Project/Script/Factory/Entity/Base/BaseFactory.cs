using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Mgfirefox.CrisisTd
{
    public class BaseFactory : AbstractFactory, IBaseFactory
    {
        private readonly BaseConfiguration configuration;

        [Inject]
        public BaseFactory(BaseConfiguration configuration, LifetimeScope parentLifetimeScope) :
            base(parentLifetimeScope)
        {
            this.configuration = configuration;
        }

        public IBaseView Create()
        {
            BaseLifetimeScope lifetimeScope =
                ParentLifetimeScope.CreateChildFromPrefabRespectSettings(configuration
                    .LifetimeScopePrefab);
            IObjectResolver lifetimeScopeContainer = lifetimeScope.Container;

            var view = lifetimeScopeContainer.Resolve<IBaseView>();

            BaseData data = BaseData.CreateBuilder()
                .FromConfiguration(configuration.DataConfiguration).Build();

            var presenter = lifetimeScopeContainer.Resolve<IBasePresenter>();
            presenter.Initialize(data);

            return view;
        }

        public bool TryCreate(out IBaseView view)
        {
            try
            {
                view = Create();

                return true;
            }
            catch (Exception e)
            {
                if (e is not (InvalidArgumentException or ConfigurationNotFoundException))
                {
                    Debug.LogException(new CaughtUnexpectedException(e));
                }
            }

            view = null;

            return false;
        }
    }
}

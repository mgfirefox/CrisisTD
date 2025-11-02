using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Mgfirefox.CrisisTd
{
    public class EnemyFactory : AbstractFactory, IEnemyFactory
    {
        private readonly EnemyConfiguration configuration;

        [Inject]
        public EnemyFactory(EnemyConfiguration configuration, LifetimeScope parentLifetimeScope) :
            base(parentLifetimeScope)
        {
            this.configuration = configuration;
        }

        public IEnemyView Create(Vector3 position, Quaternion orientation)
        {
            EnemyLifetimeScope lifetimeScope =
                ParentLifetimeScope.CreateChildFromPrefabRespectSettings(configuration
                    .LifetimeScopePrefab);
            IObjectResolver lifetimeScopeContainer = lifetimeScope.Container;

            var view = lifetimeScopeContainer.Resolve<IEnemyView>();

            EnemyData data = EnemyData.CreateBuilder()
                .FromConfiguration(configuration.DataConfiguration).WithPosition(position)
                .WithOrientation(orientation).Build();

            var presenter = lifetimeScopeContainer.Resolve<IEnemyPresenter>();
            presenter.Initialize(data);

            return view;
        }

        public bool TryCreate(Vector3 position, Quaternion orientation, out IEnemyView view)
        {
            try
            {
                view = Create(position, orientation);

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

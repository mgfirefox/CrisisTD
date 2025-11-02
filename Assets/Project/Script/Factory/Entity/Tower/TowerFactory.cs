using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Mgfirefox.CrisisTd
{
    public class TowerFactory : AbstractFactory, ITowerFactory
    {
        private readonly IReadOnlyDictionary<TowerId, TowerConfiguration> configurations;

        [Inject]
        public TowerFactory(IReadOnlyDictionary<TowerId, TowerConfiguration> configurations,
            LifetimeScope parentLifetimeScope) : base(parentLifetimeScope)
        {
            this.configurations = configurations;
        }

        public ITowerView Create(TowerId id, Vector3 position, Quaternion orientation)
        {
            if (!TowerIdValidator.TryValidate(id))
            {
                throw new InvalidArgumentException(nameof(id), id.ToString());
            }

            if (configurations.TryGetValue(id, out TowerConfiguration configuration))
            {
                TowerLifetimeScope lifetimeScope =
                    ParentLifetimeScope.CreateChildFromPrefabRespectSettings(configuration
                        .LifetimeScopePrefab);
                IObjectResolver lifetimeScopeContainer = lifetimeScope.Container;

                var view = lifetimeScopeContainer.Resolve<ITowerView>();

                TowerData data = TowerData.CreateBuilder()
                    .FromConfiguration(configuration.DataConfiguration).WithId(id)
                    .WithPosition(position).WithOrientation(orientation).Build();

                var presenter = lifetimeScopeContainer.Resolve<ITowerPresenter>();
                presenter.Initialize(data);

                return view;
            }

            throw new ConfigurationNotFoundException(id.ToString(),
                typeof(TowerConfiguration).ToString());
        }

        public bool TryCreate(TowerId id, Vector3 position, Quaternion orientation,
            out ITowerView view)
        {
            try
            {
                view = Create(id, position, orientation);

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

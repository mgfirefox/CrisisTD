using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Mgfirefox.CrisisTd
{
    public class EnemyFactory : AbstractFactory, IEnemyFactory
    {
        private readonly IReadOnlyDictionary<EnemyId, EnemyConfiguration> configurations;

        [Inject]
        public EnemyFactory(IReadOnlyDictionary<EnemyId, EnemyConfiguration> configurations, LifetimeScope parentLifetimeScope) :
            base(parentLifetimeScope)
        {
            this.configurations = configurations;
        }

        public IEnemyView Create(EnemyId id, Vector3 position, Quaternion orientation)
        {
            if (!EnemyIdValidator.TryValidate(id))
            {
                throw new InvalidArgumentException(nameof(id), id.ToString());
            }
            
            if (configurations.TryGetValue(id, out EnemyConfiguration configuration))
            {
                EnemyLifetimeScope lifetimeScope =
                    ParentLifetimeScope.CreateChildFromPrefabRespectSettings(configuration
                        .LifetimeScopePrefab);
                IObjectResolver lifetimeScopeContainer = lifetimeScope.Container;

                var view = lifetimeScopeContainer.Resolve<IEnemyView>();

                IModelComponent model =
                    CreateModel(configuration.ModelPrefab, lifetimeScopeContainer, view);

                EnemyData data = EnemyData.CreateBuilder()
                    .FromConfiguration(configuration.DataConfiguration).WithId(id)
                    .WithPosition(position).WithOrientation(orientation).Build();

                var presenter = lifetimeScopeContainer.Resolve<IEnemyPresenter>();
                presenter.Initialize(data);

                return view;
            }
            
            throw new ConfigurationNotFoundException(id.ToString(),
                typeof(EnemyConfiguration).ToString());
        }

        public bool TryCreate(EnemyId id, Vector3 position, Quaternion orientation, out IEnemyView view)
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
        
        private IModelComponent CreateModel(ModelComponent prefab,
            IObjectResolver lifetimeScopeContainer, IEnemyView view)
        {
            IModelComponent model = lifetimeScopeContainer.Instantiate(prefab);
                
            view.AddChild(model);

            return model;
        }
    }
}

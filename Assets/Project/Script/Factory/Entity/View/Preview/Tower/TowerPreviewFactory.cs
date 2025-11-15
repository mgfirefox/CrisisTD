using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Mgfirefox.CrisisTd
{
    public class TowerPreviewFactory : AbstractFactory, ITowerPreviewFactory
    {
        private readonly IRangeViewFactory rangeViewFactory;

        private readonly IReadOnlyDictionary<TowerId, TowerConfiguration> configurations;

        private readonly TowerPreviewView previewViewPrefab;

        [Inject]
        public TowerPreviewFactory(IReadOnlyDictionary<TowerId, TowerConfiguration> configurations,
            TowerPreviewView previewViewPrefab, IRangeViewFactory rangeViewFactory,
            LifetimeScope parentLifetimeScope) : base(parentLifetimeScope)
        {
            this.configurations = configurations;
            this.previewViewPrefab = previewViewPrefab;
            this.rangeViewFactory = rangeViewFactory;
        }

        public ITowerPreviewView Create(TowerId id)
        {
            if (!TowerIdValidator.TryValidate(id))
            {
                throw new InvalidArgumentException(nameof(id), id.ToString());
            }

            if (configurations.TryGetValue(id, out TowerConfiguration configuration))
            {
                ITowerPreviewView view =
                    ParentLifetimeScope.Container.Instantiate(previewViewPrefab);
                view.Initialize();

                var level = new BranchLevel();
                
                if (configuration.DataConfiguration.LevelDataConfigurations.TryGetValue(
                        level, out LevelDataConfiguration levelDataConfiguration))
                {
                    foreach (AbstractTowerActionDataConfiguration actionDataConfiguration in
                             levelDataConfiguration.TowerActionDataConfigurations)
                    {
                        if (rangeViewFactory.TryCreate(configuration.DataConfiguration.Type,
                                actionDataConfiguration, view, out IRangeView rangeView))
                        {
                            view.AddChild(rangeView);
                        }
                        else
                        {
                            // TODO: Change Warning
                            Debug.LogWarning(
                                $"Failed to create Object of type {typeof(IRangeView)} with ID \"${configuration.DataConfiguration.Type}\"");
                        }
                    }
                }

                if (configuration.ModelPrefabs.TryGetValue(level, out ModelComponent modelPrefab))
                {
                    IModelComponent model = ParentLifetimeScope.Container.Instantiate(modelPrefab);
                    model.Initialize();
                    
                    model.Layer = 0;
                    
                    view.AddChild(model);

                    view.Model = model;
                }

                return view;
            }

            throw new ConfigurationNotFoundException(id.ToString(),
                typeof(TowerConfiguration).ToString());
        }

        public bool TryCreate(TowerId id, out ITowerPreviewView view)
        {
            try
            {
                view = Create(id);

                return true;
            }
            catch (Exception e)
            {
                if (e is not (InvalidArgumentException or PrefabNotFoundException))
                {
                    Debug.LogException(new CaughtUnexpectedException(e));
                }
            }

            view = null;

            return false;
        }
    }
}

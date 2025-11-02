using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Mgfirefox.CrisisTd
{
    public class TowerPlacementActionFactory : AbstractFactory, ITowerPlacementActionFactory
    {
        private readonly TowerPlacementActionLifetimeScope lifetimeScopePrefab;

        [Inject]
        public TowerPlacementActionFactory(TowerPlacementActionLifetimeScope lifetimeScopePrefab,
            LifetimeScope parentLifetimeScope) : base(parentLifetimeScope)
        {
            this.lifetimeScopePrefab = lifetimeScopePrefab;
        }

        public ITowerPlacementAction Create(TowerPlacementActionData data, IUnitySceneObject parent)
        {
            TowerPlacementActionLifetimeScope lifetimeScope =
                ParentLifetimeScope.CreateChildFromPrefabRespectSettings(lifetimeScopePrefab);
            IObjectResolver lifetimeScopeContainer = lifetimeScope.Container;

            var action = lifetimeScopeContainer.Resolve<ITowerPlacementAction>();
            action.Initialize(data);

            var view = lifetimeScopeContainer.Resolve<ITowerPlacementActionView>();

            parent.AddChild(view);

            return action;
        }

        public bool TryCreate(TowerPlacementActionData data, IUnitySceneObject parent,
            out ITowerPlacementAction action)
        {
            try
            {
                action = Create(data, parent);

                return true;
            }
            catch (Exception e)
            {
                if (e is not (InvalidArgumentException or PrefabNotFoundException))
                {
                    Debug.LogException(new CaughtUnexpectedException(e));
                }
            }

            action = null;

            return false;
        }
    }
}

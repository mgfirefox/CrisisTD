using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Mgfirefox.CrisisTd
{
    public class TowerInteractionActionFactory : AbstractFactory, ITowerInteractionActionFactory
    {
        private readonly TowerInteractionActionLifetimeScope lifetimeScopePrefab;

        [Inject]
        public TowerInteractionActionFactory(
            TowerInteractionActionLifetimeScope lifetimeScopePrefab,
            LifetimeScope parentLifetimeScope) : base(parentLifetimeScope)
        {
            this.lifetimeScopePrefab = lifetimeScopePrefab;
        }

        public ITowerInteractionAction Create(TowerInteractionActionData data,
            IUnitySceneObject parent)
        {
            TowerInteractionActionLifetimeScope lifetimeScope =
                ParentLifetimeScope.CreateChildFromPrefabRespectSettings(lifetimeScopePrefab);
            IObjectResolver lifetimeScopeContainer = lifetimeScope.Container;

            var action = lifetimeScopeContainer.Resolve<ITowerInteractionAction>();
            action.Initialize(data);

            var view = lifetimeScopeContainer.Resolve<ITowerInteractionActionView>();

            parent.AddChild(view);

            return action;
        }

        public bool TryCreate(TowerInteractionActionData data, IUnitySceneObject parent,
            out ITowerInteractionAction action)
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

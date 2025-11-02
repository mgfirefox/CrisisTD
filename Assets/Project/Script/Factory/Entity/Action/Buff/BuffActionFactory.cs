using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Mgfirefox.CrisisTd
{
    public class BuffActionFactory : AbstractFactory, IBuffActionFactory
    {
        private readonly IReadOnlyDictionary<BuffActionType, AbstractBuffActionLifetimeScope>
            lifetimeScopePrefabs;

        [Inject]
        public BuffActionFactory(
            IReadOnlyDictionary<BuffActionType, AbstractBuffActionLifetimeScope>
                lifetimeScopePrefabs, LifetimeScope parentLifetimeScope) : base(parentLifetimeScope)
        {
            this.lifetimeScopePrefabs = lifetimeScopePrefabs;
        }

        public IBuffAction Create(AbstractBuffActionData data, IUnitySceneObject parent)
        {
            if (!BuffActionTypeValidator.TryValidate(data.Type))
            {
                throw new InvalidArgumentException(nameof(data), data.ToString());
            }

            switch (data.Type)
            {
                case BuffActionType.Constant:
                    var constantData = data.Cast<ConstantBuffActionData>();

                    return CreateConstant(constantData, parent);
                case BuffActionType.Undefined:
                default:
                    throw new InvalidArgumentException(nameof(data), data.ToString());
            }
        }

        private IConstantBuffAction CreateConstant(ConstantBuffActionData constantData,
            IUnitySceneObject parent)
        {
            if (lifetimeScopePrefabs.TryGetValue(BuffActionType.Constant,
                    out AbstractBuffActionLifetimeScope lifetimeScopePrefab))
            {
                var constantLifetimeScopePrefab =
                    lifetimeScopePrefab.Cast<ConstantBuffActionLifetimeScope>();

                ConstantBuffActionLifetimeScope lifetimeScope =
                    ParentLifetimeScope.CreateChildFromPrefabRespectSettings(
                        constantLifetimeScopePrefab);
                IObjectResolver lifetimeScopeContainer = lifetimeScope.Container;

                var action = lifetimeScopeContainer.Resolve<IConstantBuffAction>();
                action.Initialize(constantData);

                var view = lifetimeScopeContainer.Resolve<IConstantBuffActionView>();

                parent.AddChild(view);

                return action;
            }

            throw new PrefabNotFoundException(nameof(BuffActionType.Constant),
                typeof(ConstantBuffActionLifetimeScope).ToString());
        }

        public bool TryCreate(AbstractBuffActionData data, IUnitySceneObject parent,
            out IBuffAction action)
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

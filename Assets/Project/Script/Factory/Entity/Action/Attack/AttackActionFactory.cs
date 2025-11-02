using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Mgfirefox.CrisisTd
{
    public class AttackActionFactory : AbstractFactory, IAttackActionFactory
    {
        private readonly IReadOnlyDictionary<AttackActionType, AbstractAttackActionLifetimeScope>
            lifetimeScopePrefabs;

        [Inject]
        public AttackActionFactory(
            IReadOnlyDictionary<AttackActionType, AbstractAttackActionLifetimeScope>
                lifetimeScopePrefabs, LifetimeScope parentLifetimeScope) : base(parentLifetimeScope)
        {
            this.lifetimeScopePrefabs = lifetimeScopePrefabs;
        }

        public IAttackAction Create(AbstractAttackActionData data, IUnitySceneObject parent)
        {
            if (!AttackActionTypeValidator.TryValidate(data.Type))
            {
                throw new InvalidArgumentException(nameof(data), data.ToString());
            }

            switch (data.Type)
            {
                case AttackActionType.SingleTarget:
                    var singleTargetData = data.Cast<SingleTargetAttackActionData>();

                    return CreateSingleTarget(singleTargetData, parent);
                case AttackActionType.Area:
                    var areaData = data.Cast<AreaAttackActionData>();

                    return CreateArea(areaData, parent);
                case AttackActionType.ArcAngle:
                    var arcAngleData = data.Cast<ArcAngleAttackActionData>();

                    return CreateArcAngle(arcAngleData, parent);
                case AttackActionType.Burst:
                    var multipleTargetData = data.Cast<BurstAttackActionData>();

                    return CreateMultipleTarget(multipleTargetData, parent);
                case AttackActionType.Undefined:
                default:
                    throw new InvalidArgumentException(nameof(data), data.ToString());
            }
        }

        public bool TryCreate(AbstractAttackActionData data, IUnitySceneObject parent,
            out IAttackAction action)
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

        private ISingleTargetAttackAction CreateSingleTarget(
            SingleTargetAttackActionData singleTargetData, IUnitySceneObject parent)
        {
            if (lifetimeScopePrefabs.TryGetValue(AttackActionType.SingleTarget,
                    out AbstractAttackActionLifetimeScope lifetimeScopePrefab))
            {
                var singleTargetLifetimeScopePrefab =
                    lifetimeScopePrefab.Cast<SingleTargetAttackActionLifetimeScope>();

                SingleTargetAttackActionLifetimeScope lifetimeScope =
                    ParentLifetimeScope.CreateChildFromPrefabRespectSettings(
                        singleTargetLifetimeScopePrefab);
                IObjectResolver lifetimeScopeContainer = lifetimeScope.Container;

                var action = lifetimeScopeContainer.Resolve<ISingleTargetAttackAction>();
                action.Initialize(singleTargetData);

                var view = lifetimeScopeContainer.Resolve<ISingleTargetAttackActionView>();

                parent.AddChild(view);

                return action;
            }

            throw new PrefabNotFoundException(nameof(AttackActionType.SingleTarget),
                typeof(SingleTargetAttackActionLifetimeScope).ToString());
        }

        private IAreaAttackAction CreateArea(AreaAttackActionData areaData,
            IUnitySceneObject parent)
        {
            if (lifetimeScopePrefabs.TryGetValue(AttackActionType.Area,
                    out AbstractAttackActionLifetimeScope lifetimeScopePrefab))
            {
                var areaLifetimeScopePrefab =
                    lifetimeScopePrefab.Cast<AreaAttackActionLifetimeScope>();

                AreaAttackActionLifetimeScope lifetimeScope =
                    ParentLifetimeScope.CreateChildFromPrefabRespectSettings(
                        areaLifetimeScopePrefab);
                IObjectResolver lifetimeScopeContainer = lifetimeScope.Container;

                var action = lifetimeScopeContainer.Resolve<IAreaAttackAction>();
                action.Initialize(areaData);

                var view = lifetimeScopeContainer.Resolve<IAreaAttackActionView>();

                parent.AddChild(view);

                return action;
            }

            throw new PrefabNotFoundException(nameof(AttackActionType.Area),
                typeof(AreaAttackActionLifetimeScope).ToString());
        }

        private IArcAngleAttackAction CreateArcAngle(ArcAngleAttackActionData arcAngleData,
            IUnitySceneObject parent)
        {
            if (lifetimeScopePrefabs.TryGetValue(AttackActionType.ArcAngle,
                    out AbstractAttackActionLifetimeScope lifetimeScopePrefab))
            {
                var arcAngleTargetLifetimeScopePrefab =
                    lifetimeScopePrefab.Cast<ArcAngleAttackActionLifetimeScope>();

                ArcAngleAttackActionLifetimeScope lifetimeScope =
                    ParentLifetimeScope.CreateChildFromPrefabRespectSettings(
                        arcAngleTargetLifetimeScopePrefab);
                IObjectResolver lifetimeScopeContainer = lifetimeScope.Container;

                var action = lifetimeScopeContainer.Resolve<IArcAngleAttackAction>();
                action.Initialize(arcAngleData);

                var view = lifetimeScopeContainer.Resolve<IArcAngleAttackActionView>();

                parent.AddChild(view);

                return action;
            }

            throw new PrefabNotFoundException(nameof(AttackActionType.ArcAngle),
                typeof(ArcAngleAttackActionLifetimeScope).ToString());
        }

        private IBurstAttackAction CreateMultipleTarget(BurstAttackActionData burstData,
            IUnitySceneObject parent)
        {
            if (lifetimeScopePrefabs.TryGetValue(AttackActionType.Burst,
                    out AbstractAttackActionLifetimeScope lifetimeScopePrefab))
            {
                var multipleTargetLifetimeScopePrefab =
                    lifetimeScopePrefab.Cast<BurstAttackActionLifetimeScope>();

                BurstAttackActionLifetimeScope lifetimeScope =
                    ParentLifetimeScope.CreateChildFromPrefabRespectSettings(
                        multipleTargetLifetimeScopePrefab);
                IObjectResolver lifetimeScopeContainer = lifetimeScope.Container;

                var action = lifetimeScopeContainer.Resolve<IBurstAttackAction>();
                action.Initialize(burstData);

                var view = lifetimeScopeContainer.Resolve<IBurstAttackActionView>();

                parent.AddChild(view);

                return action;
            }

            throw new PrefabNotFoundException(nameof(AttackActionType.Burst),
                typeof(BurstAttackActionLifetimeScope).ToString());
        }
    }
}

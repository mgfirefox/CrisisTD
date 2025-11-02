using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Mgfirefox.CrisisTd
{
    public class TowerActionFactory : AbstractFactory, ITowerActionFactory
    {
        private readonly IReadOnlyDictionary<TowerType, AbstractTowerActionLifetimeScope>
            lifetimeScopePrefabs;

        [Inject]
        public TowerActionFactory(
            IReadOnlyDictionary<TowerType, AbstractTowerActionLifetimeScope> lifetimeScopePrefabs,
            LifetimeScope parentLifetimeScope) : base(parentLifetimeScope)
        {
            this.lifetimeScopePrefabs = lifetimeScopePrefabs;
        }

        public ITowerAction Create(TowerType type, AbstractTowerActionData data,
            IUnitySceneObject parent)
        {
            if (!TowerTypeValidator.TryValidate(type))
            {
                throw new InvalidArgumentException(nameof(type), type.ToString());
            }

            switch (type)
            {
                case TowerType.Attack:
                    var attackData = data.Cast<AttackTowerActionData>();

                    return CreateAttack(attackData, parent);
                case TowerType.Support:
                    var supportData = data.Cast<SupportTowerActionData>();

                    return CreateSupport(supportData, parent);
                case TowerType.Undefined:
                default:
                    throw new InvalidArgumentException(nameof(type), type.ToString());
            }
        }

        public bool TryCreate(TowerType type, AbstractTowerActionData data,
            IUnitySceneObject parent, out ITowerAction action)
        {
            try
            {
                action = Create(type, data, parent);

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

        private IAttackTowerAction CreateAttack(AttackTowerActionData attackData,
            IUnitySceneObject parent)
        {
            if (lifetimeScopePrefabs.TryGetValue(TowerType.Attack,
                    out AbstractTowerActionLifetimeScope lifetimeScopePrefab))
            {
                var attackLifetimeScopePrefab =
                    lifetimeScopePrefab.Cast<AttackTowerActionLifetimeScope>();

                AttackTowerActionLifetimeScope lifetimeScope =
                    ParentLifetimeScope.CreateChildFromPrefabRespectSettings(
                        attackLifetimeScopePrefab);
                IObjectResolver lifetimeScopeContainer = lifetimeScope.Container;

                var action = lifetimeScopeContainer.Resolve<IAttackTowerAction>();
                action.Initialize(attackData);

                var view = lifetimeScopeContainer.Resolve<IAttackTowerActionView>();

                parent.AddChild(view);

                return action;
            }

            throw new PrefabNotFoundException(nameof(TowerType.Attack),
                typeof(AttackTowerActionLifetimeScope).ToString());
        }

        private ISupportTowerAction CreateSupport(SupportTowerActionData supportData,
            IUnitySceneObject parent)
        {
            if (lifetimeScopePrefabs.TryGetValue(TowerType.Support,
                    out AbstractTowerActionLifetimeScope lifetimeScopePrefab))
            {
                var supportLifetimeScopePrefab =
                    lifetimeScopePrefab.Cast<SupportTowerActionLifetimeScope>();

                SupportTowerActionLifetimeScope lifetimeScope =
                    ParentLifetimeScope.CreateChildFromPrefabRespectSettings(
                        supportLifetimeScopePrefab);
                IObjectResolver lifetimeScopeContainer = lifetimeScope.Container;

                var action = lifetimeScopeContainer.Resolve<ISupportTowerAction>();
                action.Initialize(supportData);

                var view = lifetimeScopeContainer.Resolve<ISupportTowerActionView>();

                parent.AddChild(view);

                return action;
            }

            throw new PrefabNotFoundException(nameof(TowerType.Support),
                typeof(SupportTowerActionLifetimeScope).ToString());
        }
    }
}

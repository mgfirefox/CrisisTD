using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Mgfirefox.CrisisTd
{
    public class RangeViewFactory : AbstractFactory, IRangeViewFactory
    {
        private readonly IReadOnlyDictionary<RangeType, AbstractRangeView> viewPrefabs;

        [Inject]
        public RangeViewFactory(IReadOnlyDictionary<RangeType, AbstractRangeView> viewPrefabs,
            LifetimeScope parentLifetimeScope) : base(parentLifetimeScope)
        {
            this.viewPrefabs = viewPrefabs;
        }

        public IRangeView Create(TowerType towerType,
            AbstractTowerActionDataConfiguration towerActionDataConfiguration,
            IUnitySceneObject parent)
        {
            switch (towerType)
            {
                case TowerType.Attack:
                    var attackTowerActionDataConfiguration = towerActionDataConfiguration
                        .Cast<AttackTowerActionDataConfiguration>();

                    return CreateEnemyTarget(attackTowerActionDataConfiguration, parent);
                case TowerType.Support:
                    var supportTowerActionDataConfiguration = towerActionDataConfiguration
                        .Cast<SupportTowerActionDataConfiguration>();

                    return CreateTowerTarget(supportTowerActionDataConfiguration, parent);
                case TowerType.Undefined:
                default:
                    throw new InvalidArgumentException(nameof(towerType), towerType.ToString());
            }
        }

        public bool TryCreate(TowerType towerType,
            AbstractTowerActionDataConfiguration towerActionDataConfiguration,
            IUnitySceneObject parent, out IRangeView view)
        {
            try
            {
                view = Create(towerType, towerActionDataConfiguration, parent);

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

        private IEnemyTargetRangeView CreateEnemyTarget(
            AttackTowerActionDataConfiguration attackTowerActionDataConfiguration,
            IUnitySceneObject parent)
        {
            if (viewPrefabs.TryGetValue(RangeType.EnemyTarget, out AbstractRangeView viewPrefab))
            {
                var enemyTargetViewPrefab = viewPrefab.Cast<EnemyTargetRangeView>();

                IEnemyTargetRangeView view =
                    ParentLifetimeScope.Container.Instantiate(enemyTargetViewPrefab);
                view.Initialize();
                view.Radius = attackTowerActionDataConfiguration.Range;

                parent.AddChild(view);

                return view;
            }

            throw new PrefabNotFoundException(nameof(RangeType.EnemyTarget),
                typeof(EnemyTargetRangeView).ToString());
        }

        private ITowerTargetRangeView CreateTowerTarget(
            SupportTowerActionDataConfiguration supportTowerActionDataConfiguration,
            IUnitySceneObject parent)
        {
            if (viewPrefabs.TryGetValue(RangeType.TowerTarget, out AbstractRangeView viewPrefab))
            {
                var towerTargetViewPrefab = viewPrefab.Cast<TowerTargetRangeView>();

                ITowerTargetRangeView view =
                    ParentLifetimeScope.Container.Instantiate(towerTargetViewPrefab);
                view.Initialize();
                view.Radius = supportTowerActionDataConfiguration.Range;

                parent.AddChild(view);

                return view;
            }

            throw new PrefabNotFoundException(nameof(RangeType.TowerTarget),
                typeof(TowerTargetRangeView).ToString());
        }
    }
}

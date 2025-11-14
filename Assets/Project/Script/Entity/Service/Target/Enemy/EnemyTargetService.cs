using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class EnemyTargetService : AbstractTargetService<IEnemyView>, IEnemyTargetService
    {
        private readonly IRouteService routeService;

        [Inject]
        public EnemyTargetService(ITowerTransformService transformService,
            IEnemyTargetRangeView rangeView, IRouteService routeService,
            ITowerAllEffectService effectService, Scene scene) : base(transformService,
            effectService, rangeView, scene)
        {
            this.routeService = routeService;
        }

        public IList<IEnemyView> SortFirstTargets(IList<IEnemyView> targets)
        {
            if (targets == null)
            {
                throw new InvalidArgumentException(nameof(targets), null);
            }

            return targets.OrderByDescending(target => target.WaypointIndex)
                .ThenBy(ClosestTargetToNextWaypointSelector).ToList();
        }

        public IList<IEnemyView> SortLastTargets(IList<IEnemyView> targets)
        {
            if (targets == null)
            {
                throw new InvalidArgumentException(nameof(targets), null);
            }

            return targets.OrderBy(target => target.WaypointIndex)
                .ThenByDescending(ClosestTargetToNextWaypointSelector).ToList();
        }

        public IList<IEnemyView> SortStrongestTargets(IList<IEnemyView> targets)
        {
            if (targets == null)
            {
                throw new InvalidArgumentException(nameof(targets), null);
            }

            return targets.OrderByDescending(HealthSelector).ToList();
        }

        public IList<IEnemyView> SortWeakestTargets(IList<IEnemyView> targets)
        {
            if (targets == null)
            {
                throw new InvalidArgumentException(nameof(targets), null);
            }

            return targets.OrderBy(HealthSelector).ToList();
        }

        protected override IList<IEnemyView> SortTargetsByPriority(IList<IEnemyView> targets)
        {
            targets = base.SortTargetsByPriority(targets);

            if (TargetPriority != TargetPriority.Last)
            {
                targets = SortFirstTargets(targets);
            }

            return TargetPriority switch
            {
                TargetPriority.Last => SortLastTargets(targets),
                TargetPriority.Farthest => SortFarthestTargets(targets),
                TargetPriority.Closest => SortClosestTargets(targets),
                TargetPriority.Strongest => SortStrongestTargets(targets),
                TargetPriority.Weakest => SortWeakestTargets(targets),
                TargetPriority.Random => SortRandomTargets(targets),
                var _ => targets,
            };
        }

        private float ClosestTargetToNextWaypointSelector(IEnemyView target)
        {
            float epsilon = Scene.Settings.MathSettings.Epsilon;

            try
            {
                try
                {
                    Vector3 nextWaypoint = routeService.GetNextWaypoint(target.WaypointIndex,
                        target.Position, epsilon);

                    return Vector3Utility.GetSqrDistance(nextWaypoint, target.Position);
                }
                catch (RestorableInvalidArgumentException)
                {
                    Vector3 restoredPosition =
                        routeService.RestorePosition(target.WaypointIndex, target.Position,
                            epsilon);

                    Vector3 nextWaypoint = routeService.GetNextWaypoint(target.WaypointIndex,
                        restoredPosition, epsilon);

                    return Vector3Utility.GetSqrDistance(nextWaypoint, restoredPosition);
                }
            }
            catch (InvalidArgumentException)
            {
                return float.MaxValue;
            }
        }

        private float HealthSelector(IEnemyView target)
        {
            return target.Health;
        }
    }
}

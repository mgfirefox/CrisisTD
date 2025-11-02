using System;
using UnityEngine;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class EnemyTransformService : AbstractMovingTransformService<EnemyTransformServiceData>,
        IEnemyTransformService
    {
        private readonly IRouteService routeService;

        private readonly ITranslationService translationService;
        private readonly IRotationService rotationService;

        public int WaypointIndex { get; private set; }

        public event Action BaseReached;

        [Inject]
        public EnemyTransformService(ITranslationService translationService,
            IRotationService rotationService, IRouteService routeService, Scene scene) : base(scene)
        {
            this.translationService = translationService;
            this.rotationService = rotationService;
            this.routeService = routeService;
        }

        public void Move()
        {
            float epsilon = Scene.Settings.MathSettings.Epsilon;

            float travelledDistance = 0.0f;

            try
            {
                try
                {
                    while (!routeService.IsLastWaypoint(WaypointIndex))
                    {
                        Vector3 nextWaypoint =
                            routeService.GetNextWaypoint(WaypointIndex, Position, epsilon);

                        TranslationToResult result = translationService.TranslateTo(Position,
                            nextWaypoint, MaxMovementSpeed, epsilon, travelledDistance);

                        Position = result.position;
                        travelledDistance = result.travelledDistance;

                        if (!Position.EqualsApproximately(nextWaypoint, epsilon))
                        {
                            Yaw = rotationService.RotateTo(Position, nextWaypoint).eulerAngles.y;

                            return;
                        }

                        WaypointIndex++;
                    }

                    BaseReached?.Invoke();
                }
                catch (RestorableInvalidArgumentException)
                {
                    Position = routeService.RestorePosition(WaypointIndex, Position, epsilon);

                    Debug.LogWarning(
                        Warning.InvalidArgumentRestoredMessage("position", Position.ToString()));
                }
                catch (Exception e)
                {
                    if (e is not (PrefabNotFoundException or InvalidArgumentException))
                    {
                        throw new CaughtUnexpectedException(e);
                    }

                    throw;
                }
            }
            catch (InvalidArgumentException)
            {
            }
        }
    }
}

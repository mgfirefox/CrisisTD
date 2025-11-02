using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class RouteService : AbstractService, IRouteService
    {
        private readonly IMapService mapService;

        [Inject]
        public RouteService(IMapService mapService)
        {
            this.mapService = mapService;
        }

        public Vector3 GetNextWaypoint(int index, Vector3 position, float epsilon)
        {
            if (!mapService.IsLoaded)
            {
                throw new MapNotLoadedException();
            }

            bool isLastWaypoint = IsLastWaypoint(index);

            IReadOnlyList<Vector3> waypoints = mapService.Waypoints;
            if (index < 0 || index >= waypoints.Count)
            {
                throw new RestorableInvalidArgumentException(nameof(index), index.ToString());
            }

            Vector3 nextWaypoint = isLastWaypoint ? waypoints[index] : waypoints[index + 1];
            if (!Vector3Utility.IsPointOnLineSegment(position, waypoints[index], nextWaypoint,
                    epsilon))
            {
                throw new RestorableInvalidArgumentException(nameof(position), position.ToString());
            }

            return nextWaypoint;
        }

        public bool IsLastWaypoint(int index)
        {
            IReadOnlyList<Vector3> waypoints = mapService.Waypoints;

            if (waypoints.Count == 0)
            {
                throw new InvalidArgumentException(nameof(index), index.ToString());
            }

            return index == waypoints.Count - 1;
        }

        public Vector3 RestorePosition(int index, Vector3 position, float epsilon)
        {
            IReadOnlyList<Vector3> waypoints = mapService.Waypoints;
            if (index < 0 && index >= waypoints.Count)
            {
                throw new InvalidArgumentException(nameof(index), index.ToString());
            }
            if (waypoints.Count == 0)
            {
                throw new InvalidArgumentException(nameof(index), index.ToString());
            }
            if (waypoints.Count == 1)
            {
                return waypoints[0];
            }
            if (index == waypoints.Count - 1)
            {
                return waypoints[^1];
            }

            Vector3 restoredPosition = Vector3Utility.ProjectPointOnLineSegment(position,
                waypoints[index], waypoints[index + 1], epsilon);
            if (!Vector3Utility.IsPointOnLineSegment(restoredPosition, waypoints[index],
                    waypoints[index + 1], epsilon))
            {
                throw new InvalidArgumentException(nameof(position), position.ToString());
            }

            return restoredPosition;
        }
    }
}

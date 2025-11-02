using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class MapService : AbstractService, IMapService
    {
        private readonly IRotationService rotationService;

        private readonly IMapViewFactory viewFactory;

        private IMapView view;

        private readonly IList<Vector3> waypoints = new List<Vector3>();

        public MapId Id { get; private set; } = MapId.Undefined;

        public IReadOnlyList<Vector3> Waypoints => waypoints.AsReadOnly();
        public Pose EnemySpawnPose { get; private set; } = Pose.identity;
        public Pose BasePose { get; private set; } = Pose.identity;

        public Pose PlayerSpawnPose { get; private set; } = Pose.identity;

        public bool IsLoaded => MapIdValidator.TryValidate(Id);

        [Inject]
        public MapService(IMapViewFactory viewFactory, IRotationService rotationService)
        {
            this.viewFactory = viewFactory;
            this.rotationService = rotationService;
        }

        public void Load(MapId id)
        {
            if (IsLoaded)
            {
                Unload();
            }

            try
            {
                view = viewFactory.Create(id);
            }
            catch (InvalidArgumentException e)
            {
                throw new RestorableInvalidArgumentException(nameof(id), id.ToString(), e);
            }
            catch (Exception e)
            {
                if (e is not PrefabNotFoundException)
                {
                    throw new CaughtUnexpectedException(e);
                }

                throw;
            }

            Id = id;

            LoadWaypoints();

            switch (Waypoints.Count)
            {
                case 0:
                break;
                case 1:
                {
                    var pose = new Pose
                    {
                        position = Waypoints[0],
                    };

                    EnemySpawnPose = pose;
                    BasePose = pose;

                    break;
                }
                default:
                {
                    EnemySpawnPose = new Pose(Waypoints[0],
                        rotationService.RotateTo(Waypoints[0], Waypoints[1]));
                    BasePose = new Pose(Waypoints[0],
                        rotationService.RotateTo(Waypoints[^1], Waypoints[^2]));

                    break;
                }
            }

            PlayerSpawnPose = view.PlayerSpawnPose;
        }

        public bool TryLoad(MapId id)
        {
            try
            {
                Load(id);

                return true;
            }
            catch (Exception e)
            {
                if (e is not (RestorableInvalidArgumentException or PrefabNotFoundException))
                {
                    Debug.LogException(new CaughtUnexpectedException(e));
                }
            }

            return false;
        }

        public void Unload()
        {
            view.Destroy();
            view = null;

            Id = MapId.Undefined;

            waypoints.Clear();
            EnemySpawnPose = Pose.identity;
            BasePose = Pose.identity;

            PlayerSpawnPose = Pose.identity;
        }

        private void LoadWaypoints()
        {
            IReadOnlyList<IBezierSegmentView> bezierSegments = view.BezierSegmentFolder.Children;
            foreach (IBezierSegmentView bezierSegment in bezierSegments)
            {
                if (waypoints.Count == 0)
                {
                    waypoints.Add(bezierSegment.StartPointPosition);
                }

                switch (bezierSegment.Type)
                {
                    case BezierType.Linear:
                        waypoints.Add(bezierSegment.EndPointPosition);
                    break;
                    case BezierType.Quadratic:
                        Vector3 p0 = bezierSegment.StartPointPosition;
                        Vector3 p1 = bezierSegment.ControlPointFolder.ChildTransforms[0].position;
                        Vector3 p2 = bezierSegment.EndPointPosition;

                        for (int i = 1; i <= bezierSegment.SegmentCount; i++)
                        {
                            float t = i / (float)bezierSegment.SegmentCount;

                            Vector3 point = MathUtility.GetQuadraticBezierPoint(p0, p1, p2, t);

                            waypoints.Add(point);
                        }
                    break;
                    case BezierType.Cubic:
                        IReadOnlyList<Transform> controlPointTransforms =
                            bezierSegment.ControlPointFolder.ChildTransforms;

                        p0 = bezierSegment.StartPointPosition;
                        p1 = controlPointTransforms[0].position;
                        p2 = controlPointTransforms[1].position;
                        Vector3 p3 = bezierSegment.EndPointPosition;

                        for (int i = 1; i <= bezierSegment.SegmentCount; i++)
                        {
                            float t = i / (float)bezierSegment.SegmentCount;

                            Vector3 point = MathUtility.GetCubicBezierPoint(p0, p1, p2, p3, t);

                            waypoints.Add(point);
                        }
                    break;
                    case BezierType.Undefined:
                    default:
                        // TODO: Change Exception
                        throw new Exception();
                }
            }
        }
    }
}

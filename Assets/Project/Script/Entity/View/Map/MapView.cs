using System.Linq;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class MapView : AbstractView, IMapView
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private Model obstacleModel;
        
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private Transform playerSpawn;

        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private BezierSegmentFolder bezierSegmentFolder;
        
        protected IModelObject ObstacleModel => obstacleModel;

        public Pose PlayerSpawnPose =>
            new(playerSpawn.transform.position, playerSpawn.transform.rotation);

        public IBezierSegmentFolder BezierSegmentFolder => bezierSegmentFolder;

#if UNITY_EDITOR
        public void OnDrawGizmos()
        {
            IBezierSegmentView[] bezierSegments = bezierSegmentFolder.Transform
                .GetComponentsInChildren<IBezierSegmentView>();

            Color oldColor = Gizmos.color;
            Matrix4x4 oldMatrix = Gizmos.matrix;

            Gizmos.matrix = Matrix4x4.identity;

            foreach (IBezierSegmentView bezierSegment in bezierSegments)
            {
                Gizmos.color = Color.black;

                Vector3[] controlPointPositions = bezierSegment.ControlPointFolder.Transform
                    .GetComponentsInChildren<Transform>().Skip(1)
                    .Select(childTransform => childTransform.position).ToArray();

                GizmosUtility.DrawBezierCurve(bezierSegment.StartPointPosition,
                    bezierSegment.EndPointPosition, controlPointPositions,
                    bezierSegment.SegmentCount);

                Gizmos.color = Color.white;

                switch (bezierSegment.Type)
                {
                    case BezierType.Quadratic:
                        Gizmos.DrawLine(bezierSegment.StartPointPosition,
                            bezierSegment.ControlPointFolder.Transform.GetChild(0).position);
                        Gizmos.DrawLine(bezierSegment.EndPointPosition,
                            bezierSegment.ControlPointFolder.Transform.GetChild(0).position);
                    break;
                    case BezierType.Cubic:
                        Gizmos.DrawLine(bezierSegment.StartPointPosition,
                            bezierSegment.ControlPointFolder.Transform.GetChild(0).position);
                        Gizmos.DrawLine(bezierSegment.EndPointPosition,
                            bezierSegment.ControlPointFolder.Transform.GetChild(1).position);
                    break;
                    case BezierType.Linear:
                    case BezierType.Undefined:
                    default:
                    break;
                }
            }

            Gizmos.color = oldColor;
            Gizmos.matrix = oldMatrix;
        }

        [Button]
        private void FixSegmentStartPositions()
        {
            IBezierSegmentView[] bezierSegments = bezierSegmentFolder.Transform
                .GetComponentsInChildren<IBezierSegmentView>();
            if (bezierSegments.Length < 2)
            {
                return;
            }

            bool hasChanged = false;

            for (int i = 1; i < bezierSegments.Length; i++)
            {
                IBezierSegmentView segment = bezierSegments[i];
                IBezierSegmentView prevSegment = bezierSegments[i - 1];

                Transform segmentStartTransform = segment.Transform.GetChild(0);
                Vector3 targetPosition = prevSegment.EndPointPosition;

                if (!segmentStartTransform.position.Equals(targetPosition))
                {
                    Undo.RecordObject(transform, "Fix Bezier Segment Start Position");

                    segmentStartTransform.position = targetPosition;

                    hasChanged = true;
                }
            }

            if (!hasChanged)
            {
                return;
            }

            EditorUtility.SetDirty(bezierSegmentFolder.Transform);
        }
#endif

        protected override void OnInitialized()
        {
            base.OnInitialized();
            
            obstacleModel.Initialize();

            bezierSegmentFolder.Initialize();
        }

        protected override void OnDestroying()
        {
            bezierSegmentFolder.Destroy();
            
            obstacleModel.Destroy();

            base.OnDestroying();
        }
    }
}

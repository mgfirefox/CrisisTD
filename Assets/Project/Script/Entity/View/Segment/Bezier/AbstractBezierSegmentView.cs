using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractBezierSegmentView : AbstractView, IBezierSegmentView
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private Transform startPoint;
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private Transform endPoint;

        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private BasicFolder controlPointFolder;

        [SerializeField]
        [BoxGroup("BezierSegment")]
        [MinValue(1)]
        [MaxValue(64)]
        private int segmentCount;

        public abstract BezierType Type { get; }

        public Vector3 StartPointPosition => startPoint.position;
        public Vector3 EndPointPosition => endPoint.position;

        public IBasicFolder ControlPointFolder => controlPointFolder;

        public int SegmentCount => segmentCount;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            controlPointFolder.Initialize();
        }

        protected override void OnDestroying()
        {
            controlPointFolder.Destroy();

            base.OnDestroying();
        }
    }
}

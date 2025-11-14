using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IMapView : IView
    {
        Pose PlayerSpawnPose { get; }

        IBezierSegmentFolder BezierSegmentFolder { get; }
    }
}

using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IMapView : IVisualView
    {
        Pose PlayerSpawnPose { get; }

        IBezierSegmentFolderView BezierSegmentFolder { get; }
    }
}

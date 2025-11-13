using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IBezierSegmentView : IView
    {
        BezierType Type { get; }

        Vector3 StartPointPosition { get; }
        Vector3 EndPointPosition { get; }

        IBasicFolder ControlPointFolder { get; }

        int SegmentCount { get; }
    }
}

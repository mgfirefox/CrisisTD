using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface ITowerPreviewView : IView, ITransformModel
    {
        IRangeFolder RangeFolder { get; }

        new Vector3 Position { get; set; }
        new Quaternion Orientation { get; set; }

        Vector3 ObstacleSize { set; }
    }
}

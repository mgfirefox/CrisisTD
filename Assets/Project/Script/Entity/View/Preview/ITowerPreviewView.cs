using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface ITowerPreviewView : IVisualView, ITransformModel
    {
        IRangeFolder RangeFolder { get; }

        new Vector3 Position { get; set; }
        new Quaternion Orientation { get; set; }
    }
}

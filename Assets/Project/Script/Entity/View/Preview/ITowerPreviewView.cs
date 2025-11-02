using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface ITowerPreviewView : IVisualView, ITransformModel
    {
        IRangeFolderView RangeViewFolder { get; }

        new Vector3 Position { get; set; }
        new Quaternion Orientation { get; set; }
    }
}

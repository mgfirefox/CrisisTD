using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IVirtualCameraView : IView, ITransformModel
    {
        new Vector3 Position { get; set; }
        new Quaternion Orientation { get; set; }

        int Priority { get; set; }
    }
}

using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface ICameraView : IView, ITransformModel
    {
        new Vector3 Position { get; set; }
        new Quaternion Orientation { get; set; }

        Ray GetRayFromScreenPosition(Vector3 screenPosition);
    }
}

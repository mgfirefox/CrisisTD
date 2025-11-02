using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class TransformServiceData : AbstractServiceData, ITransformModel
    {
        public Vector3 Position { get; set; } = Vector3.zero;
        public Quaternion Orientation { get; set; } = Quaternion.identity;
    }
}

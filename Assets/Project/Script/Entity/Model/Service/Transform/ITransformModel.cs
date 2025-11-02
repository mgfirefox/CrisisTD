using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface ITransformModel : IModel
    {
        public Vector3 Position { get; }
        public Quaternion Orientation { get; }
    }
}

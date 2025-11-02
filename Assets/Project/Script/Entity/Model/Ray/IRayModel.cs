using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IRayModel : IModel
    {
        Vector3 StartPosition { get; }
        Vector3 EndPosition { get; }
        Vector3 Direction { get; }

        float MaxDistance { get; }

        LayerMask CollisionLayerMask { get; }
    }
}

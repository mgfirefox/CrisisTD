using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface ICollisionView : IView
    {
        LayerMask CollisionLayerMask { get; }
    }
}

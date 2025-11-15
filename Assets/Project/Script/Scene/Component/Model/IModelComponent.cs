using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IModelComponent : IComponent
    {
        int Layer { get; set; }
        LayerMask CollisionLayerMask { get; }

        Vector3 PivotPoint { get; }

        bool IsHidden { get; }

        void Show();
        void Hide();
    }
}

using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IModelObject : IUnitySceneObject
    {
        int Layer { get; }
        LayerMask CollisionLayerMask { get; }

        Vector3 PivotPoint { get; }

        bool IsHidden { get; }

        void Show();
        void Hide();
    }
}

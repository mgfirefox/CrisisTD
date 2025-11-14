using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IView : IUnitySceneObject, IModel
    {
        int Layer { get; }
        LayerMask CollisionLayerMask { get; }
        
        Vector3 PivotPoint { get; }

        bool IsHidden { get; }

        event Action Showing;
        event Action Hiding;

        void Show();
        void Hide();
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IFolderView : IView
    {
        IReadOnlyList<Transform> ChildTransforms { get; }
    }

    public interface IFolderView<out TIItem> : IFolderView
        where TIItem : class, IUnitySceneObject
    {
        IReadOnlyList<TIItem> Children { get; }
    }
}

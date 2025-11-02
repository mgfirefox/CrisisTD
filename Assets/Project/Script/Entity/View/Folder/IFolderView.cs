using System.Collections.Generic;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IFolderView : IView
    {
        IReadOnlyList<Transform> ChildTransforms { get; }
    }

    public interface IFolderView<out TIItemView> : IFolderView
        where TIItemView : class, IView
    {
        IReadOnlyList<TIItemView> Children { get; }
    }
}

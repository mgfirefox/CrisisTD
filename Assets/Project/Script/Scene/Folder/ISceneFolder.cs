using System.Collections.Generic;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface ISceneFolder : IUnitySceneObject
    {
        IReadOnlyList<Transform> ChildTransforms { get; }
    }

    public interface ISceneFolder<out TIItem> : ISceneFolder
        where TIItem : class, IUnitySceneObject
    {
        IReadOnlyList<TIItem> Children { get; }
    }
}

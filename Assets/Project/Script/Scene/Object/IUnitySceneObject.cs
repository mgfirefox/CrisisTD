using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IUnitySceneObject : ISceneObject
    {
        Transform Transform { get; }
        Transform Parent { get; }

        bool IsDestroying { get; }

        void Initialize();

        void AddChild(IUnitySceneObject child);
        bool TryRemoveChild(IUnitySceneObject child);
    }
}

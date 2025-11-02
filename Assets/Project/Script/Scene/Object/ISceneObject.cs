using System;

namespace Mgfirefox.CrisisTd
{
    public interface ISceneObject
    {
        bool IsDestroyed { get; }

        event Action Destroying;

        void Destroy();
    }

    public interface ISceneObject<in TData> : ISceneObject
        where TData : AbstractData
    {
        bool IsInitialized { get; }

        void Initialize(TData data);
    }
}

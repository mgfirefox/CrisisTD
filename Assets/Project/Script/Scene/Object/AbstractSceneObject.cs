using System;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractSceneObject : ISceneObject
    {
        protected Scene Scene { get; }

        public bool IsDestroyed { get; private set; }

        public event Action Destroying;

        protected AbstractSceneObject(Scene scene)
        {
            Scene = scene;
        }

        protected void Initialize()
        {
            if (IsDestroyed)
            {
                // TODO: Change Exception
                throw new Exception($"Destroyed Object \"{this}\" cannot be initialized again.");
            }

            Scene.Loop.AddObject(this);
        }

        public void Destroy()
        {
            if (IsDestroyed)
            {
                return;
            }

            Destroying?.Invoke();

            OnDestroying();

            Scene.Loop.RemoveObject(this);

            IsDestroyed = true;
        }

        protected virtual void OnDestroying()
        {
        }
    }

    public abstract class AbstractSceneObject<TData> : AbstractSceneObject, ISceneObject<TData>
        where TData : AbstractData
    {
        public bool IsInitialized { get; private set; }

        protected AbstractSceneObject(Scene scene) : base(scene)
        {
        }

        public void Initialize(TData data)
        {
            if (IsInitialized)
            {
                // TODO: Change Exception
                throw new Exception($"Initialized Object \"{this}\" cannot be initialized again.");
            }

            base.Initialize();

            if (data == null)
            {
                throw new InvalidArgumentException(nameof(data), null);
            }

            OnInitialized(data);

            IsInitialized = true;
        }

        protected virtual void OnInitialized(TData data)
        {
        }
    }
}

using System;
using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractUnitySceneObject : MonoBehaviour, IUnitySceneObject
    {
        [SerializeField]
        [BoxGroup("SceneObject")]
        [ReadOnly]
        private bool isInitialized;
        [SerializeField]
        [BoxGroup("SceneObject")]
        [ReadOnly]
        private bool isDestroyed;

        public Transform Transform => transform;
        public Transform Parent => transform.parent;

        public bool IsInitialized { get => isInitialized; private set => isInitialized = value; }
        public bool IsDestroying { get; private set; }
        public bool IsDestroyed { get => isDestroyed; private set => isDestroyed = value; }

        public event Action Destroying;

        protected virtual IUnitySceneObject GetChildParent(IUnitySceneObject child)
        {
            return null;
        }

        public void OnDestroy()
        {
            Destroy();
        }

        public void Initialize()
        {
            if (IsDestroyed)
            {
                // TODO: Change Exception
                throw new Exception($"Destroyed Object \"{this}\" cannot be initialized again.");
            }

            if (IsInitialized)
            {
                // TODO: Change Exception
                throw new Exception($"Initialized Object \"{this}\" cannot be initialized again.");
            }

            OnInitialized();

            IsInitialized = true;
        }

        public void Destroy()
        {
            if (IsDestroyed)
            {
                return;
            }

            IsDestroying = true;

            Destroying?.Invoke();

            OnDestroying();

            IsDestroyed = true;
        }

        public void AddChild(IUnitySceneObject child)
        {
            if (child.IsDestroyed)
            {
                return;
            }

            IUnitySceneObject parent = GetChildParent(child);
            if (parent == null)
            {
                child.Transform.SetParent(Transform);

                OnChildAdded(child);

                return;
            }
            if (child.Transform.parent != null)
            {
                if (child.Parent == parent.Transform)
                {
                    return;
                }

                if (child.Transform.parent.TryGetComponent(out IUnitySceneObject sceneObject))
                {
                    sceneObject.TryRemoveChild(child);
                }
            }

            parent.AddChild(child);

            OnChildAdded(child);
        }

        public bool TryRemoveChild(IUnitySceneObject child)
        {
            if (child.IsDestroyed)
            {
                return false;
            }

            IUnitySceneObject parent = GetChildParent(child);
            if (parent != null)
            {
                return parent.TryRemoveChild(child);
            }

            foreach (Transform childTransform in Transform)
            {
                if (childTransform != child.Transform)
                {
                    continue;
                }

                if (!child.IsDestroying)
                {
                    child.Transform.SetParent(null);
                }

                OnChildRemoved(child);

                return true;
            }

            return false;
        }

        protected virtual void OnInitialized()
        {
        }

        protected virtual void OnDestroying()
        {
            if (Parent == null)
            {
            }
            else
            {
                IUnitySceneObject[] sceneObjects = Parent.GetComponents<IUnitySceneObject>();
                if (sceneObjects.Length != 0)
                {
                    if (sceneObjects.Length > 1)
                    {
                        sceneObjects[1].TryRemoveChild(this);
                    }
                    else
                    {
                        sceneObjects[0].TryRemoveChild(this);
                    }
                }
            }

            Destroy(gameObject);
        }

        protected virtual void OnChildAdded(IUnitySceneObject child)
        {
        }

        protected virtual void OnChildRemoved(IUnitySceneObject child)
        {
        }
    }
}

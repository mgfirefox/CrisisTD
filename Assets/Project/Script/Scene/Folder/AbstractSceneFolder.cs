using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractSceneFolder : AbstractUnitySceneObject, ISceneFolder
    {
        private readonly IList<Transform> childTransforms = new List<Transform>();

        public IReadOnlyList<Transform> ChildTransforms => childTransforms.AsReadOnly();

        protected override void OnInitialized()
        {
            base.OnInitialized();

            InitializeChildTransforms();
        }

        private void InitializeChildTransforms()
        {
            childTransforms.AddRange(Transform.GetComponentsInChildren<Transform>()
                .Where(childTransform => childTransform.parent == Transform));
        }

        protected override void OnDestroying()
        {
            ClearChildTransforms();

            base.OnDestroying();
        }

        private void ClearChildTransforms()
        {
            childTransforms.Clear();
        }

        protected override void OnChildAdded(IUnitySceneObject child)
        {
            base.OnChildAdded(child);

            if (childTransforms.Contains(child.Transform))
            {
                return;
            }

            childTransforms.Add(child.Transform);
        }

        protected override void OnChildRemoved(IUnitySceneObject child)
        {
            base.OnChildRemoved(child);

            childTransforms.Remove(child.Transform);
        }
    }

    public abstract class AbstractSceneFolder<TIItem> : AbstractSceneFolder, ISceneFolder<TIItem>
        where TIItem : class, IUnitySceneObject
    {
        private readonly IList<TIItem> children = new List<TIItem>();

        public IReadOnlyList<TIItem> Children => children.AsReadOnly();

        protected override void OnInitialized()
        {
            base.OnInitialized();

            foreach (Transform childTransform in ChildTransforms)
            {
                if (childTransform.TryGetComponent(out TIItem item))
                {
                    item.Initialize();

                    children.Add(item);
                }
            }
        }

        protected override void OnDestroying()
        {
            children.Clear();

            base.OnDestroying();
        }

        protected override void OnChildAdded(IUnitySceneObject child)
        {
            base.OnChildAdded(child);

            if (child.Transform.TryGetComponent(out TIItem item))
            {
                if (children.Contains(item))
                {
                    return;
                }

                children.Add(item);
            }
        }

        protected override void OnChildRemoved(IUnitySceneObject child)
        {
            base.OnChildRemoved(child);

            if (child.Transform.TryGetComponent(out TIItem item))
            {
                children.Remove(item);
            }
        }
    }
}

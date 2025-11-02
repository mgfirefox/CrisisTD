using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractFolderView : AbstractView, IFolderView
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

    public abstract class AbstractFolderView<TIItemView> : AbstractFolderView,
        IFolderView<TIItemView>
        where TIItemView : class, IView
    {
        private readonly IList<TIItemView> children = new List<TIItemView>();

        public IReadOnlyList<TIItemView> Children => children.AsReadOnly();

        protected override void OnInitialized()
        {
            base.OnInitialized();

            foreach (Transform childTransform in ChildTransforms)
            {
                if (childTransform.TryGetComponent(out TIItemView item))
                {
                    item.Initialize();

                    children.Add(item);

                    continue;
                }

                // TODO: Change Warning
                Debug.LogWarning(
                    $"Object {childTransform.gameObject} is missing Component of type {typeof(TIItemView)}.",
                    childTransform.gameObject);
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

            if (child.Transform.TryGetComponent(out TIItemView item))
            {
                if (children.Contains(item))
                {
                    return;
                }

                children.Add(item);

                return;
            }

            // TODO: Change Warning
            Debug.LogWarning(
                $"Object {child.Transform.gameObject} is missing Component of type {typeof(TIItemView)}.",
                child.Transform.gameObject);
        }

        protected override void OnChildRemoved(IUnitySceneObject child)
        {
            base.OnChildRemoved(child);

            if (child.Transform.TryGetComponent(out TIItemView item))
            {
                children.Remove(item);

                return;
            }

            // TODO: Change Warning
            Debug.LogWarning(
                $"Object {child.Transform.gameObject} is missing Component of type {typeof(TIItemView)}.",
                child.Transform.gameObject);
        }
    }
}

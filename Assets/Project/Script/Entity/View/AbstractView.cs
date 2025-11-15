using System;
using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractView : AbstractUnitySceneObject, IView
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        private Model model;

        protected IModelObject Model => model;

        public int Layer => model?.Layer ?? gameObject.layer;
        public LayerMask CollisionLayerMask =>
            model?.CollisionLayerMask ?? LayerMaskUtility.GetCollisionLayerMask(gameObject.layer);

        public Vector3 PivotPoint => model?.PivotPoint ?? Vector3.zero;

        public bool IsHidden => model?.IsHidden ?? false;

        public event Action Showing;
        public event Action Hiding;

        public void Show()
        {
            if (!IsHidden)
            {
                return;
            }

            Showing?.Invoke();

            OnShowing();
        }

        public void Hide()
        {
            if (IsHidden)
            {
                return;
            }

            Hiding?.Invoke();

            OnHiding();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            model?.Initialize();
        }

        protected override void OnDestroying()
        {
            model?.Destroy();

            base.OnDestroying();
        }

        protected virtual void OnShowing()
        {
            model?.Show();
        }

        protected virtual void OnHiding()
        {
            model?.Hide();
        }
    }
}

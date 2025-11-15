using System;
using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractView : AbstractUnitySceneObject, IView
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        private ModelComponent model;

        public IModelComponent Model { get; set; }

        public int Layer => Model?.Layer ?? 0;
        public LayerMask CollisionLayerMask =>
            Model?.CollisionLayerMask ?? LayerMaskUtility.GetCollisionLayerMask(Layer);

        public Vector3 PivotPoint => Model?.PivotPoint ?? Vector3.zero;

        public bool IsHidden => Model?.IsHidden ?? false;

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

            Model = model;
            Model?.Initialize();
        }

        protected override void OnDestroying()
        {
            Model?.Destroy();

            base.OnDestroying();
        }

        protected virtual void OnShowing()
        {
            Model?.Show();
        }

        protected virtual void OnHiding()
        {
            Model?.Hide();
        }
    }
}

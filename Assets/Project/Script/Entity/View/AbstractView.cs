using System;
using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractView : AbstractUnitySceneObject, IView
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private ModelFolder modelFolder;
        
        public IModelFolder ModelFolder => modelFolder;

        public IModelComponent Model { get; set; }

        public int Layer => Model?.Layer ?? 0;
        public LayerMask CollisionLayerMask =>
            Model?.CollisionLayerMask ?? LayerMaskUtility.GetCollisionLayerMask(Layer);

        public Vector3 PivotPoint => Model?.PivotPoint ?? Vector3.zero;

        public bool IsHidden => Model?.IsHidden ?? false;

        protected override IUnitySceneObject GetChildParent(IUnitySceneObject child)
        {
            if (child.Transform.TryGetComponent(out IModelComponent _))
            {
                return modelFolder;
            }
            
            return base.GetChildParent(child);
        }

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
            
            modelFolder.Initialize();

            if (modelFolder.Children.Count > 0)
            {
                Model = modelFolder.Children[0];
            }
        }

        protected override void OnDestroying()
        {
            modelFolder.Destroy();
            
            Model = null;

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

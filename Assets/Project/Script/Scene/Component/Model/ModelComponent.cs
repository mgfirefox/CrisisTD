using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class ModelComponent : AbstractComponent, IModelComponent
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private ColliderFolder colliderFolder;
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private MeshFolder meshFolder;
        
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private AnimatorFolder animatorFolder;
        
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private Transform pivotPoint;

        [SerializeField]
        [BoxGroup("Model")]
        [ReadOnly]
        private int layer;
        [SerializeField]
        [BoxGroup("Model")]
        [ReadOnly]
        private LayerMask collisionLayerMask;

        [SerializeField]
        [BoxGroup("Model")]
        [ReadOnly]
        private bool isHidden;

        private int ColliderLayer
        {
            set
            {
                foreach (IColliderComponent collider in colliderFolder.Children)
                {
                    collider.Layer = value;
                }
            }
        }
        
        public IAnimatorFolder AnimatorFolder => animatorFolder;

        public int Layer
        {
            get => layer;
            set
            {
                layer = value;
                
                collisionLayerMask = LayerMaskUtility.GetCollisionLayerMask(layer);

                if (IsDestroyed)
                {
                    return;
                }
                
                gameObject.layer = layer;
                
                ColliderLayer = layer;
            }
        }
        public LayerMask CollisionLayerMask => collisionLayerMask;

        public Vector3 PivotPoint
        {
            get
            {
                if (IsDestroyed)
                {
                    return Vector3.zero;
                }
                
                return pivotPoint.localPosition * Transform.localScale.y;
            }
        }
        public bool IsHidden => isHidden;

        public void Show()
        {
            foreach (IMeshComponent mesh in meshFolder.Children)
            {
                mesh.Show();
            }

            isHidden = false;
        }

        public void Hide()
        {
            foreach (IMeshComponent mesh in meshFolder.Children)
            {
                mesh.Hide();
            }

            isHidden = true;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            colliderFolder.Initialize();
            
            Layer = gameObject.layer;
            
            meshFolder.Initialize();
            
            animatorFolder.Initialize();
        }

        protected override void OnDestroying()
        {
            animatorFolder.Destroy();
            
            meshFolder.Destroy();

            colliderFolder.Destroy();

            base.OnDestroying();
        }
    }
}

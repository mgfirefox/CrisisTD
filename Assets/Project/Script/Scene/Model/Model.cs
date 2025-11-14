using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class Model : AbstractUnitySceneObject, IModelObject
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
        private Vector3 pivotPoint;
        
        [SerializeField]
        [BoxGroup("Model")]
        [ReadOnly]
        private bool isHidden;
        
        public int Layer => layer;
        public LayerMask CollisionLayerMask => collisionLayerMask;
        
        public Vector3 PivotPoint => pivotPoint;
        
        public bool IsHidden => isHidden;

        public void Show()
        {
            foreach (IMesh mesh in meshFolder.Children)
            {
                mesh.Show();
            }

            isHidden = false;
        }

        public void Hide()
        {
            foreach (IMesh mesh in meshFolder.Children)
            {
                mesh.Hide();
            }
            
            isHidden = true;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            
            layer = gameObject.layer;
            collisionLayerMask = LayerMaskUtility.GetCollisionLayerMask(gameObject.layer);
            
            pivotPoint = Transform.localPosition;

            colliderFolder.Initialize();
            
            foreach (ICollider collider in colliderFolder.Children)
            {
                collider.Layer = layer;
            }
            
            meshFolder.Initialize();
        }
        
        protected override void OnDestroying()
        {
            meshFolder.Destroy();
            
            colliderFolder.Destroy();
            
            base.OnDestroying();
        }
    }
}

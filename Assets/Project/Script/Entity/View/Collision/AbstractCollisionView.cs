using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class AbstractCollisionView : AbstractView, ICollisionView
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private GameObject collision;

        [SerializeField]
        [BoxGroup("Collision")]
        [ReadOnly]
        private int layer;
        [SerializeField]
        [BoxGroup("Collision")]
        [ReadOnly]
        private LayerMask collisionLayerMask;

        protected GameObject Collision => collision;

        public int Layer => layer;
        public LayerMask CollisionLayerMask => collisionLayerMask;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            layer = collision.layer;
            collisionLayerMask = LayerMaskUtility.GetCollisionLayerMask(collision.layer);
        }
    }
}

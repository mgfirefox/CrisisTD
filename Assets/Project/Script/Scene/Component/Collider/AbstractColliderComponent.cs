using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractColliderComponent : AbstractComponent, IColliderComponent
    {
        public abstract int Layer { get; set; }

        public abstract Vector3 GetClosestPosition(Vector3 position);

        public abstract bool IsPositionWithin(Vector3 position, float epsilon);
    }

    public abstract class AbstractColliderComponent<TCollider> : AbstractColliderComponent
        where TCollider : Collider
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private new TCollider collider;

        [SerializeField]
        [BoxGroup("Collider")]
        [ReadOnly]
        private int layer;

        protected TCollider Collider => collider;

        public override int Layer
        {
            get => layer;
            set
            {
                layer = value;

                if (IsDestroyed)
                {
                    return;
                }

                collider.gameObject.layer = layer;
            }
        }

        public override Vector3 GetClosestPosition(Vector3 position)
        {
            return collider.ClosestPoint(position);
        }

        public override bool IsPositionWithin(Vector3 position, float epsilon)
        {
            return GetClosestPosition(position).EqualsApproximately(position, epsilon);
        }
    }
}

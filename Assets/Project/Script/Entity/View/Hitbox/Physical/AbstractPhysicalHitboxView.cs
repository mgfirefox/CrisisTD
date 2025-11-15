using System;
using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractPhysicalHitboxView : AbstractHitboxView, IPhysicalHitboxView
    {
        public abstract Vector3 GetClosestPosition(Vector3 position);

        public abstract bool IsPositionWithin(Vector3 position, float epsilon);
    }

    public abstract class
        AbstractPhysicalHitboxView<TICollider, TCollider> : AbstractPhysicalHitboxView
        where TICollider : class, ICollider
        where TCollider : AbstractCollider, TICollider
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private new TCollider collider;

        protected TICollider Collider => collider;

        public override Vector3 GetClosestPosition(Vector3 position)
        {
            return collider.GetClosestPosition(position);
        }

        public override bool IsPositionWithin(Vector3 position, float epsilon)
        {
            return collider.IsPositionWithin(position, epsilon);
        }
    }

    public abstract class AbstractPhysicalHitboxView<TICollider, TCollider, TITargetView> :
        AbstractPhysicalHitboxView<TICollider, TCollider>, IPhysicalHitboxView<TITargetView>
        where TICollider : class, ICollider
        where TCollider : AbstractCollider, TICollider
        where TITargetView : class, IView
    {
        public event Action<TITargetView> TargetEntered;
        public event Action<TITargetView> TargetExited;

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponentInParent(out TITargetView target))
            {
                TargetEntered?.Invoke(target);

                return;
            }

            // TODO: Change Warning
            Debug.LogWarning(
                $"Object {other.gameObject} is missing Component of type {typeof(TITargetView)}.",
                other.gameObject);
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponentInParent(out TITargetView target))
            {
                TargetExited?.Invoke(target);

                return;
            }

            // TODO: Change Warning
            Debug.LogWarning(
                $"Object {other.gameObject} is missing Component of type {typeof(TITargetView)}.",
                other.gameObject);
        }
    }
}

using System;
using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractRangeView : AbstractVisualView, IRangeView
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private new BoxCollider collider;

        [SerializeField]
        [BoxGroup("Range")]
        [ReadOnly]
        private float radius;

        public float Radius
        {
            get => radius;
            set
            {
                radius = value;

                if (IsDestroyed)
                {
                    return;
                }

                float diameter = 2 * radius;

                collider.size = new Vector3(diameter, collider.size.y, diameter);
            }
        }

        public void OnDrawGizmos()
        {
            if (IsHidden)
            {
                return;
            }

            if (!(Radius > 0.0f))
            {
                return;
            }

            Color oldColor = Gizmos.color;
            Matrix4x4 oldMatrix = Gizmos.matrix;

            var scale = new Vector3(1.0f, Constant.epsilon, 1.0f);

            Gizmos.color = Color.blue;
            Gizmos.matrix = Matrix4x4.TRS(Transform.position, Quaternion.identity, scale);

            Gizmos.DrawWireSphere(Vector3.zero, Radius);

            Gizmos.color = oldColor;
            Gizmos.matrix = oldMatrix;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Vector3 colliderSize = collider.size;
            colliderSize.y = Constant.rangeHeight;

            collider.size = colliderSize;
        }
    }

    public abstract class AbstractRangeView<TITargetView> : AbstractRangeView,
        IRangeView<TITargetView>
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

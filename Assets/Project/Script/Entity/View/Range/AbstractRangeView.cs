using System;
using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractRangeView : AbstractView, IRangeView
    {
        [SerializeField]
        [BoxGroup("Range")]
        [ReadOnly]
        private float radius;

        private Transform visual;

        public virtual float Radius
        {
            get => radius;
            set
            {
                radius = value;
                
                float diameter = 2 * value;
                
                visual.localScale = new Vector3(diameter, visual.localScale.y, diameter);
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
            
            visual = ModelFolder.Children[0].MeshFolder.Children[0].Transform;
            visual.localPosition = new Vector3(0.0f, 2 * Constant.epsilon, 0.0f);
            visual.localScale = new Vector3(0.0f, Constant.epsilon, 0.0f);
        }
    }

    public abstract class AbstractRangeView<TITargetView, THitboxView> : AbstractRangeView,
        IRangeView<TITargetView>
        where TITargetView : class, IView
        where THitboxView : AbstractBoxPhysicalHitboxView<TITargetView>
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private THitboxView hitbox;

        public override float Radius
        {
            get => base.Radius;
            set
            {
                base.Radius = value;

                if (IsDestroyed)
                {
                    return;
                }

                float diameter = 2 * value;

                hitbox.Size = new Vector3(diameter, hitbox.Height, diameter);
            }
        }

        public event Action<TITargetView> TargetEntered;
        public event Action<TITargetView> TargetExited;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            hitbox.Initialize();

            hitbox.Height = Constant.rangeHeight;

            hitbox.TargetEntered += OnHitboxTargetEntered;
            hitbox.TargetExited += OnHitboxTargetExited;
        }

        protected override void OnDestroying()
        {
            hitbox.TargetEntered -= OnHitboxTargetEntered;
            hitbox.TargetExited -= OnHitboxTargetExited;

            hitbox.Destroy();

            base.OnDestroying();
        }

        private void OnHitboxTargetEntered(TITargetView target)
        {
            TargetEntered?.Invoke(target);
        }

        private void OnHitboxTargetExited(TITargetView target)
        {
            TargetExited?.Invoke(target);
        }
    }
}

using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractSphereLogicalHitboxView<TITargetView> :
        AbstractLogicalHitboxView<TITargetView>, ISphereLogicalHitboxView<TITargetView>
        where TITargetView : class, IView
    {
        [SerializeField]
        [BoxGroup("SphereHitbox")]
        [ReadOnly]
        private float radius;

        public float Radius { get => radius; set => radius = value; }

        public override IList<TITargetView> GetTargets()
        {
            int count =
                Physics.OverlapSphereNonAlloc(Position, Radius, ColliderBuffer, CollisionLayerMask);

            IList<TITargetView> targets = new List<TITargetView>();
            for (int i = 0; i < count; i++)
            {
                Collider collider = ColliderBuffer[i];

                if (collider.TryGetComponentInParent(out TITargetView target))
                {
                    targets.Add(target);

                    continue;
                }

                // TODO: Change Warning
                Debug.LogWarning(
                    $"Object {collider.gameObject} is missing Component of type {typeof(TITargetView)}.",
                    collider.gameObject);
            }

            return targets;
        }
    }
}

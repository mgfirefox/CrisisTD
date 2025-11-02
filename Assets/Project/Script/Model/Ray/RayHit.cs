using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    [Serializable]
    public class RayHit : ICloneable
    {
        [SerializeField]
        private Vector3 position;
        [SerializeField]
        private Vector3 normal;
        [SerializeField]
        private float distance;
        [SerializeField]
        private Collider collider;

        public Vector3 Position { get => position; set => position = value; }
        public Vector3 Normal { get => normal; set => normal = value; }
        public float Distance { get => distance; set => distance = value; }
        public Collider Collider { get => collider; set => collider = value; }

        public virtual object Clone()
        {
            var rayHit = new RayHit
            {
                position = position,
                normal = normal,
                distance = distance,
                collider = collider,
            };

            return rayHit;
        }
    }

    [Serializable]
    public class RayHit<TITargetView> : RayHit
        where TITargetView : class, IView
    {
        [SerializeField]
        private TITargetView target;

        public TITargetView Target { get => target; set => target = value; }

        public override object Clone()
        {
            var rayHit = new RayHit<TITargetView>
            {
                Position = Position,
                Normal = Normal,
                Distance = Distance,
                Collider = Collider,
                Target = target,
            };

            return rayHit;
        }
    }
}

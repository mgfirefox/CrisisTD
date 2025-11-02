using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractRayView : AbstractCollisionView, IRayView
    {
        protected static RaycastHit[] RayCastHitBuffer { get; } =
            new RaycastHit[Constant.maxRayCastHitTargetCount];

        [SerializeField]
        [BoxGroup("Ray")]
        [ReadOnly]
        private Vector3 startPosition;
        [SerializeField]
        [BoxGroup("Ray")]
        [ReadOnly]
        private Vector3 endPosition;
        [SerializeField]
        [BoxGroup("Ray")]
        [ReadOnly]
        private Vector3 direction;

        [SerializeField]
        [BoxGroup("Ray")]
        [ReadOnly]
        private float maxDistance;

        [SerializeField]
        [BoxGroup("Ray")]
        [ReadOnly]
        private LayerMask rayCollisionLayerMask;

        public Vector3 StartPosition { get => startPosition; set => SetLine(value, endPosition); }
        public Vector3 EndPosition { get => endPosition; set => SetLine(startPosition, value); }
        public Vector3 Direction
        {
            get => direction;
            set => SetNormalizedRay(startPosition, value);
        }

        public float MaxDistance
        {
            get => maxDistance;
            set => SetRay(startPosition, endPosition, value);
        }

        public new LayerMask CollisionLayerMask
        {
            get => rayCollisionLayerMask;
            set => rayCollisionLayerMask = value;
        }

        public void SetLine(Vector3 startPosition, Vector3 endPosition)
        {
            this.startPosition = startPosition;
            this.endPosition = endPosition;
            direction = Vector3Utility.GetTranslation(this.startPosition, this.endPosition);

            maxDistance = direction.magnitude;

            direction.Normalize();

            if (IsDestroyed)
            {
                return;
            }

            Transform.position = this.startPosition;
        }

        public void SetNormalizedLine(Vector3 startPosition, Vector3 endPosition)
        {
            this.startPosition = startPosition;
            this.endPosition = endPosition;
            direction = Vector3Utility.GetDirection(this.startPosition, this.endPosition);

            if (IsDestroyed)
            {
                return;
            }

            Transform.position = this.startPosition;
        }

        public void SetRay(Vector3 startPosition, Vector3 direction)
        {
            this.startPosition = startPosition;
            this.direction = direction;
            endPosition = this.startPosition + this.direction;

            maxDistance = direction.magnitude;

            this.direction.Normalize();

            if (IsDestroyed)
            {
                return;
            }

            Transform.position = this.startPosition;
        }

        public void SetRay(Vector3 startPosition, Vector3 direction, float maxDistance)
        {
            this.startPosition = startPosition;
            this.direction = direction.normalized;
            endPosition = this.startPosition + this.direction * maxDistance;

            this.maxDistance = maxDistance;

            if (IsDestroyed)
            {
                return;
            }

            Transform.position = this.startPosition;
        }

        public void SetNormalizedRay(Vector3 startPosition, Vector3 direction)
        {
            this.startPosition = startPosition;
            this.direction = direction.normalized;
            endPosition = this.startPosition + this.direction * maxDistance;

            if (IsDestroyed)
            {
                return;
            }

            Transform.position = this.startPosition;
        }

        public bool TryCast(out RayHit hit)
        {
            if (Physics.Raycast(startPosition, direction, out RaycastHit hitInfo, maxDistance,
                    rayCollisionLayerMask))
            {
                hit = new RayHit
                {
                    Position = hitInfo.point,
                    Normal = hitInfo.normal,
                    Distance = hitInfo.distance,
                    Collider = hitInfo.collider,
                };

                return true;
            }

            hit = null;

            return false;
        }

        public IList<RayHit> CastAll()
        {
            IList<RayHit> hits = new List<RayHit>();

            int count = Physics.RaycastNonAlloc(startPosition, direction, RayCastHitBuffer,
                maxDistance, rayCollisionLayerMask);
            for (int i = 0; i < count; i++)
            {
                RaycastHit hitInfo = RayCastHitBuffer[i];

                var hit = new RayHit
                {
                    Position = hitInfo.point,
                    Normal = hitInfo.normal,
                    Distance = hitInfo.distance,
                    Collider = hitInfo.collider,
                };

                hits.Add(hit);
            }

            return hits;
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            Vector3 startPosition = Transform.position;
            Vector3 direction = Transform.rotation.eulerAngles.normalized;
            SetNormalizedRay(startPosition, direction);

            rayCollisionLayerMask = base.CollisionLayerMask;
        }
    }

    public abstract class AbstractRayView<TITargetView> : AbstractRayView, IRayView<TITargetView>
        where TITargetView : class, IView
    {
        public bool TryCastTarget(out RayHit<TITargetView> hit)
        {
            if (Physics.Raycast(StartPosition, Direction, out RaycastHit hitInfo, MaxDistance,
                    CollisionLayerMask))
            {
                if (hitInfo.collider.TryGetComponentInParent(out TITargetView target))
                {
                    hit = new RayHit<TITargetView>
                    {
                        Position = hitInfo.point,
                        Normal = hitInfo.normal,
                        Distance = hitInfo.distance,
                        Target = target,
                    };

                    return true;
                }

                // TODO: Change Warning
                Debug.LogWarning(
                    $"Object {hitInfo.collider.gameObject} is missing Component of type {typeof(TITargetView)}.",
                    hitInfo.collider.gameObject);
            }

            hit = null;

            return false;
        }

        public IList<RayHit<TITargetView>> CastAllTargets()
        {
            IList<RayHit<TITargetView>> hits = new List<RayHit<TITargetView>>();

            int count = Physics.RaycastNonAlloc(StartPosition, Direction, RayCastHitBuffer,
                MaxDistance, CollisionLayerMask);
            for (int i = 0; i < count; i++)
            {
                RaycastHit hitInfo = RayCastHitBuffer[i];

                if (hitInfo.collider.TryGetComponentInParent(out TITargetView target))
                {
                    var hit = new RayHit<TITargetView>
                    {
                        Position = hitInfo.point,
                        Normal = hitInfo.normal,
                        Distance = hitInfo.distance,
                        Target = target,
                    };

                    hits.Add(hit);

                    continue;
                }

                // TODO: Change Warning
                Debug.LogWarning(
                    $"Object {hitInfo.collider.gameObject} is missing Component of type {typeof(TITargetView)}.",
                    hitInfo.collider.gameObject);
            }

            return hits;
        }
    }
}

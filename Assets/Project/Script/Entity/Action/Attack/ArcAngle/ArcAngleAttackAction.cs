using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class ArcAngleAttackAction :
        AbstractAttackAction<ArcAngleAttackActionData, IArcAngleAttackActionView>,
        IArcAngleAttackAction
    {
        private readonly IEnemyTargetRayView rayView;

        public float ArcAngle { get; private set; }

        public int MaxPelletCount { get; private set; }
        public int MaxPelletHitCount { get; private set; }

        [Inject]
        public ArcAngleAttackAction(IArcAngleAttackActionView view,
            IEnemyTargetService targetService, ICooldownService cooldownService,
            ITowerTransformService transformService, ITowerAnimationService animationService, IEnemyTargetRayView rayView,
            Scene scene) : base(view, targetService, cooldownService, transformService, animationService, scene)
        {
            this.rayView = rayView;
        }

        protected override void PerformAttack(IReadOnlyList<IEnemyView> targets)
        {
            base.PerformAttack(targets);
            
            IEnemyView target = targets[0];

            Vector3 shotPosition = TransformService.PivotPointPosition;

            Vector3 targetPosition = target.Position + target.PivotPoint;

            Vector3 direction = Vector3Utility.GetDirection(shotPosition, targetPosition);
            Vector3 translation = direction * TargetService.RangeRadius;

            float maxSpreadDistanceX = TargetService.RangeRadius * Mathf.Tan(ArcAngle / 2);
            float maxSpreadDistanceY = 2.0f;
            for (int i = 0; i < MaxPelletCount; i++)
            {
                var spreadTranslation =
                    new Vector3(Random.Range(-maxSpreadDistanceX, maxSpreadDistanceX),
                        Random.Range(-maxSpreadDistanceY, maxSpreadDistanceY), 0.0f);
                spreadTranslation -=
                    (Vector3.Dot(direction, spreadTranslation) / direction.sqrMagnitude - 1) *
                    direction;

                Vector3 pelletTranslation = translation + spreadTranslation;
                pelletTranslation =
                    Vector3.ClampMagnitude(pelletTranslation, TargetService.RangeRadius);

                rayView.SetRay(shotPosition, pelletTranslation);

                IList<RayHit<IEnemyView>> hits = rayView.CastAllTargets();

                IList<IEnemyView> arcAngleTargets = TargetService
                    .SortClosestTargets(hits.Select(hit => hit.Target).ToList())
                    .Take(MaxPelletHitCount).ToList();
                foreach (IEnemyView arcAngleTarget in arcAngleTargets)
                {
                    arcAngleTarget.TakeDamage(Damage, ArmorPiercing);
                }
                
                AnimationService.CreateTracer(shotPosition + pelletTranslation);

                Debug.DrawRay(shotPosition, pelletTranslation, Color.red, 1.0f);
            }

            Debug.DrawRay(shotPosition, translation, Color.green, 1.0f);
            DebugUtility.DrawCone(shotPosition, targetPosition,
                new Vector2(maxSpreadDistanceX, maxSpreadDistanceY), Color.yellow, 1.0f, 16);
        }

        protected override void OnInitialized(ArcAngleAttackActionData data)
        {
            base.OnInitialized(data);

            ArcAngle = data.ArcAngle;
            MaxPelletCount = data.PelletCount;
            MaxPelletHitCount = data.MaxPelletHitCount;

            View.ArcAngle = ArcAngle * Mathf.Rad2Deg;
            View.MaxPelletCount = MaxPelletCount;
            View.MaxPelletHitCount = MaxPelletHitCount;

            rayView.Initialize();
        }

        protected override void OnDestroying()
        {
            rayView.Destroy();

            base.OnDestroying();
        }
    }
}

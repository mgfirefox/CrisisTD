using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class AreaAttackAction :
        AbstractAttackAction<AreaAttackActionData, IAreaAttackActionView>, IAreaAttackAction
    {
        private readonly IEnemyTargetSphereHitboxView sphereHitboxView;

        public float InnerRadius { get; private set; }
        public float OuterRadius { get; private set; }

        public int MaxHitCount { get; private set; }

        [Inject]
        public AreaAttackAction(IAreaAttackActionView view, IEnemyTargetService targetService,
            ICooldownService cooldownService, IEnemyTargetSphereHitboxView sphereHitboxView,
            Scene scene) : base(view, targetService, cooldownService, scene)
        {
            this.sphereHitboxView = sphereHitboxView;
        }

        protected override void PerformAttack(IReadOnlyList<IEnemyView> targets)
        {
            float epsilon = Scene.Settings.MathSettings.Epsilon;

            IEnemyView target = targets[0];

            Vector3 areaPosition = target.Position;

            sphereHitboxView.Position = areaPosition;

            IList<IEnemyView> areaTargets = sphereHitboxView.GetTargets();
            areaTargets = areaTargets.OrderBy(areaTarget =>
                Vector3Utility.GetSqrDistance(areaPosition, areaTarget.Position)).ToList();
            for (int i = 0,
                 hitCount = 0;
                 i < areaTargets.Count && hitCount < MaxHitCount;
                 i++)
            {
                IEnemyView areaTarget = areaTargets[i];

                float sqrInnerRadius = InnerRadius * InnerRadius;
                float sqrOuterRadius = OuterRadius * OuterRadius;

                float sqrDistance =
                    Vector3Utility.GetSqrDistance(areaPosition, areaTarget.Position);
                if (sqrDistance.IsLessThanOrEqualApproximately(sqrInnerRadius, epsilon))
                {
                    areaTarget.TakeDamage(Damage);

                    hitCount++;

                    continue;
                }
                if (sqrDistance.IsGreaterThanOrEqualApproximately(sqrOuterRadius, epsilon))
                {
                    continue;
                }

                float damageMultiplier = Mathf.InverseLerp(sqrOuterRadius,
                    sqrInnerRadius, sqrDistance);

                areaTarget.TakeDamage(Damage * damageMultiplier);

                hitCount++;
            }

            DebugUtility.DrawWireSphere(areaPosition, InnerRadius, Color.red, 0.5f);
            DebugUtility.DrawWireSphere(areaPosition, OuterRadius, Color.yellow, 0.5f);
        }

        protected override void OnInitialized(AreaAttackActionData data)
        {
            base.OnInitialized(data);

            InnerRadius = data.InnerRadius;
            OuterRadius = data.OuterRadius;
            MaxHitCount = data.MaxHitCount;

            View.InnerRadius = InnerRadius;
            View.OuterRadius = OuterRadius;
            View.MaxHitCount = MaxHitCount;

            sphereHitboxView.Initialize();

            sphereHitboxView.Radius = OuterRadius;
        }

        protected override void OnDestroying()
        {
            sphereHitboxView.Destroy();

            base.OnDestroying();
        }
    }
}

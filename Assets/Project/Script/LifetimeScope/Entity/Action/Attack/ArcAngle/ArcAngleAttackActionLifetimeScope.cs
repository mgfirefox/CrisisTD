using NaughtyAttributes;
using UnityEngine;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class ArcAngleAttackActionLifetimeScope : AbstractAttackActionLifetimeScope
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private EnemyTargetRayView rayView;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            Configure<ArcAngleAttackAction, ArcAngleAttackActionView>(builder);

            builder.RegisterInstance(rayView).AsImplementedInterfaces();
        }
    }
}

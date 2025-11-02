using NaughtyAttributes;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Mgfirefox.CrisisTd
{
    public class AreaAttackActionLifetimeScope : AbstractAttackActionLifetimeScope
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private EnemyTargetSphereLogicalHitboxView sphereHitboxView;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            Configure<AreaAttackAction, AreaAttackActionView>(builder);

            builder.RegisterComponent(sphereHitboxView).AsImplementedInterfaces();
        }
    }
}

using NaughtyAttributes;
using UnityEngine;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class TowerInteractionActionLifetimeScope : ActionLifetimeScope
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private TowerTargetRayView rayView;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            Configure<TowerInteractionAction, TowerInteractionActionView>(builder);

            builder.RegisterInstance(rayView).AsImplementedInterfaces();
        }
    }
}

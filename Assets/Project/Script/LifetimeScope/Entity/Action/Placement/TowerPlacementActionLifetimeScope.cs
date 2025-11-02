using NaughtyAttributes;
using UnityEngine;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class TowerPlacementActionLifetimeScope : ActionLifetimeScope
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private BasicRayView rayView;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            Configure<TowerPlacementAction, TowerPlacementActionView>(builder);

            builder.RegisterInstance(rayView).AsImplementedInterfaces();

            builder.Register<TowerPreviewFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<RangeViewFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TowerObstacleViewFactory>(Lifetime.Singleton)
                .AsImplementedInterfaces();
        }
    }
}

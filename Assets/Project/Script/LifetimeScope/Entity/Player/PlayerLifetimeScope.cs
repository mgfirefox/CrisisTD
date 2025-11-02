using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class PlayerLifetimeScope : EntityLifetimeScope<PlayerPresenter, PlayerView>
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.Register<PlayerTransformService>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<LoadoutService>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<TowerPlacementActionFactory>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            builder.Register<TowerInteractionActionFactory>(Lifetime.Singleton)
                .AsImplementedInterfaces();
        }
    }
}

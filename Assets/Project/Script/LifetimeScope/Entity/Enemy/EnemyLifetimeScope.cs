using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class EnemyLifetimeScope : EntityLifetimeScope<EnemyPresenter, EnemyView>
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.Register<EnemyTransformService>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<HealthService>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}

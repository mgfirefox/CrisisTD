using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class BaseLifetimeScope : EntityLifetimeScope<BasePresenter, BaseView>
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.Register<HealthService>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}

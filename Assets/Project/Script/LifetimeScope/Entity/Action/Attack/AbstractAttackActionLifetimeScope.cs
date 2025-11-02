using VContainer;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractAttackActionLifetimeScope : ActionLifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.Register<CooldownService>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}

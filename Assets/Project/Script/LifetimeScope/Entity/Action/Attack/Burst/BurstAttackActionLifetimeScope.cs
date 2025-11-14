using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class BurstAttackActionLifetimeScope : AbstractAttackActionLifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            Configure<BurstAttackAction, BurstAttackActionView>(builder);

            builder.Register<CooldownService>(Lifetime.Singleton).AsImplementedInterfaces()
                .Keyed("Burst");
        }
    }
}

using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class SupportTowerActionLifetimeScope : AbstractTowerActionLifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            Configure<SupportTowerAction, SupportTowerActionView, TowerTargetRangeView,
                AttackTowerTargetService>(builder);

            builder.Register<BuffActionFactory>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}

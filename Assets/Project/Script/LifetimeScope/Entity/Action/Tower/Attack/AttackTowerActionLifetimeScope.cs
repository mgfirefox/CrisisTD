using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class AttackTowerActionLifetimeScope : AbstractTowerActionLifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            Configure<AttackTowerAction, AttackTowerActionView, EnemyTargetRangeView,
                EnemyTargetService>(builder);

            builder.Register<AttackActionFactory>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}

using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class SingleTargetAttackActionLifetimeScope : AbstractAttackActionLifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            Configure<SingleTargetAttackAction, SingleTargetAttackActionView>(builder);
        }
    }
}

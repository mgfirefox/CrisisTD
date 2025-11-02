using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class ConstantBuffActionLifetimeScope : AbstractBuffActionLifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            Configure<ConstantBuffAction, ConstantBuffActionView>(builder);
        }
    }
}

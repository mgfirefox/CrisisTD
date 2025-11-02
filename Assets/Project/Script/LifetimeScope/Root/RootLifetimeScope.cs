using VContainer;
using VContainer.Unity;

namespace Mgfirefox.CrisisTd
{
    public class RootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            RegisterInputActions(builder);
        }

        private void RegisterInputActions(IContainerBuilder builder)
        {
            builder.Register<InputActions>(Lifetime.Singleton);
        }
    }
}

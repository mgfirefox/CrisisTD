using VContainer.Unity;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractFactory : IFactory
    {
        protected LifetimeScope ParentLifetimeScope { get; }

        protected AbstractFactory(LifetimeScope parentLifetimeScope)
        {
            ParentLifetimeScope = parentLifetimeScope;
        }
    }
}

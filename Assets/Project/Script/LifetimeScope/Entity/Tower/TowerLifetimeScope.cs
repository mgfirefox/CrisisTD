using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class TowerLifetimeScope : EntityLifetimeScope<TowerPresenter, TowerView>
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.Register<TowerTransformService>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<TowerBuffEffectService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TowerDebuffEffectService>(Lifetime.Singleton)
                .AsImplementedInterfaces();
            builder.Register<TowerAllEffectService>(Lifetime.Singleton).AsImplementedInterfaces();
            
            builder.Register<TowerModelService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TowerAnimationService>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<LevelService>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<TowerActionFactory>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}

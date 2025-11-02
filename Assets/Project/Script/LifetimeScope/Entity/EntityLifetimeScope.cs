using NaughtyAttributes;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Mgfirefox.CrisisTd
{
    public abstract class EntityLifetimeScope<TPresenter, TView> : LifetimeScope
        where TPresenter : class, IPresenter
        where TView : class, IView
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private TView view;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterComponent(view).AsImplementedInterfaces();

            builder.Register<TPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}

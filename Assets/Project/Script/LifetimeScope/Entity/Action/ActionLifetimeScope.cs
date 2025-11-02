using NaughtyAttributes;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Mgfirefox.CrisisTd
{
    public abstract class ActionLifetimeScope : LifetimeScope
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private AbstractActionView view;

        protected void Configure<TAction, TView>(IContainerBuilder builder)
            where TAction : class, IAction
            where TView : class, IActionView
        {
            builder.RegisterComponent(view.Cast<TView>()).AsImplementedInterfaces();

            builder.Register<TAction>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}

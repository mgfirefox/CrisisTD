using NaughtyAttributes;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Mgfirefox.CrisisTd
{
    public class AbstractTowerActionLifetimeScope : ActionLifetimeScope
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private AbstractRangeView rangeView;

        protected void Configure<TAction, TView, TRangeView, TTargetService>(
            IContainerBuilder builder)
            where TAction : class, ITowerAction
            where TView : class, ITowerActionView
            where TRangeView : class, IRangeView
            where TTargetService : class, ITargetService
        {
            Configure<TAction, TView>(builder);

            builder.RegisterComponent(rangeView.Cast<TRangeView>()).AsImplementedInterfaces();

            builder.Register<TTargetService>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}

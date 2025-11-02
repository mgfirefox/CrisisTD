using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public abstract class
        EntityConfiguration<TPresenter, TView, TLifetimeScope,
            TDataConfiguration> : ScriptableObject
        where TPresenter : class, IPresenter
        where TView : class, IView
        where TLifetimeScope : EntityLifetimeScope<TPresenter, TView>
        where TDataConfiguration : ScriptableObject
    {
        [SerializeField]
        [BoxGroup("Entity")]
        [Required]
        private TLifetimeScope lifetimeScopePrefab;
        [SerializeField]
        [BoxGroup("Entity")]
        [Required]
        private TDataConfiguration dataConfiguration;

        public TLifetimeScope LifetimeScopePrefab => lifetimeScopePrefab;
        public TDataConfiguration DataConfiguration => dataConfiguration;
    }
}

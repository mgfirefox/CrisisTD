using System.Collections.Generic;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractLogicalHitboxView : AbstractHitboxView, ILogicalHitboxView
    {
        protected static Collider[] ColliderBuffer { get; } =
            new Collider[Constant.maxHitboxTargetCount];
    }

    public abstract class AbstractLogicalHitboxView<TITargetView> : AbstractLogicalHitboxView,
        ILogicalHitboxView<TITargetView>
        where TITargetView : class, IView
    {
        public abstract IList<TITargetView> GetTargets();
    }
}

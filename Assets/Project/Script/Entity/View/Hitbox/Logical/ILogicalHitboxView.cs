using System.Collections.Generic;

namespace Mgfirefox.CrisisTd
{
    public interface ILogicalHitboxView : IHitboxView
    {
    }

    public interface ILogicalHitboxView<TITargetView> : ILogicalHitboxView
        where TITargetView : class, IView
    {
        IList<TITargetView> GetTargets();
    }
}

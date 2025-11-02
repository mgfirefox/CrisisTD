using System;

namespace Mgfirefox.CrisisTd
{
    public interface IRangeView : IVisualView, IRangeModel
    {
        new float Radius { get; set; }
    }

    public interface IRangeView<out TITargetView> : IRangeView
        where TITargetView : class, IView
    {
        event Action<TITargetView> TargetEntered;
        event Action<TITargetView> TargetExited;
    }
}

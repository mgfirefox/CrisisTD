using System;
using System.Collections.Generic;

namespace Mgfirefox.CrisisTd
{
    public interface ITargetService : IDataService<TargetServiceData>, ITargetModel
    {
        event Action<float> RangeRadiusChanged;

        void Update();
    }

    public interface ITargetService<TITargetView> : ITargetService
        where TITargetView : class, IView
    {
        IReadOnlyList<TITargetView> Targets { get; }

        IList<TITargetView> SortFarthestTargets(IList<TITargetView> targets);
        IList<TITargetView> SortClosestTargets(IList<TITargetView> targets);
        IList<TITargetView> SortRandomTargets(IList<TITargetView> targets);
    }
}

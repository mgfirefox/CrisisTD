using System.Collections.Generic;

namespace Mgfirefox.CrisisTd
{
    public interface IEnemyTargetService : ITargetService<IEnemyView>
    {
        IList<IEnemyView> SortFirstTargets(IList<IEnemyView> targets);
        IList<IEnemyView> SortLastTargets(IList<IEnemyView> targets);
        IList<IEnemyView> SortStrongestTargets(IList<IEnemyView> targets);
        IList<IEnemyView> SortWeakestTargets(IList<IEnemyView> targets);
    }
}

using System.Collections.Generic;

namespace Mgfirefox.CrisisTd
{
    public interface ILevelModel : IModel
    {
        int MaxZeroBranchIndex { get; }
        int MaxFirstBranchIndex { get; }
        int MaxSecondBranchIndex { get; }
        BranchLevel Level { get; }
        
        IReadOnlyList<NextBranchLevel> NextLevels { get; }
    }
}

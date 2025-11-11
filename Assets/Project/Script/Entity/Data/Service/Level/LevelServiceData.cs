using System.Collections.Generic;

namespace Mgfirefox.CrisisTd.Level
{
    public class LevelServiceData : AbstractServiceData
    {
        public IDictionary<BranchLevel, LevelItem> Items { get; set; } =
            new Dictionary<BranchLevel, LevelItem>();

        public BranchLevel Level { get; set; } = new();
    }
}

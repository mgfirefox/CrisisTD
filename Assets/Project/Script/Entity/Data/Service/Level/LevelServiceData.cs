using System.Collections.Generic;

namespace Mgfirefox.CrisisTd.Level
{
    public class LevelServiceData : AbstractServiceData
    {
        public IDictionary<LevelIndex, LevelItem> dataDictionary =
            new Dictionary<LevelIndex, LevelItem>();

        public LevelIndex Index { get; set; } = new();
    }
}

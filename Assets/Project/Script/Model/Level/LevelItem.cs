using System.Collections.Generic;

namespace Mgfirefox.CrisisTd
{
    public class LevelItem
    {
        public IList<AbstractTowerActionData> ActionDataList { get; set; } =
            new List<AbstractTowerActionData>();
    }
}

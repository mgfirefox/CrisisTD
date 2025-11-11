using System;
using System.Collections.Generic;

namespace Mgfirefox.CrisisTd
{
    [Serializable]
    public class LevelItem
    {
        public IList<AbstractTowerActionData> ActionDataList { get; set; } =
            new List<AbstractTowerActionData>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Mgfirefox.CrisisTd
{
    [Serializable]
    public class LevelItem : ICloneable
    {
        public IList<AbstractTowerActionData> ActionDataList { get; set; } =
            new List<AbstractTowerActionData>();

        public object Clone()
        {
            var levelItem = new LevelItem
            {
                ActionDataList = ActionDataList.ToList(),
            };

            return levelItem;
        }
    }
}

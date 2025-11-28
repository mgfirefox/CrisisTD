using System;
using System.Collections.Generic;
using System.Linq;

namespace Mgfirefox.CrisisTd
{
    [Serializable]
    public class LevelItem : ICloneable
    {
        public BranchLevel Level { get; set; } = new();
        public float UpgradeCost { get; set; }
        public IList<AbstractTowerActionData> ActionDataList { get; set; } =
            new List<AbstractTowerActionData>();

        public object Clone()
        {
            var levelItem = new LevelItem
            {
                Level = Level.Clone() as BranchLevel,
                UpgradeCost = UpgradeCost,
                ActionDataList = ActionDataList.ToList(),
            };

            return levelItem;
        }
    }
}

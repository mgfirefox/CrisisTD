using System.Collections.Generic;

namespace Mgfirefox.CrisisTd
{
    public class SupportTowerActionData : AbstractTowerActionData
    {
        public IList<AbstractBuffActionData> ActionDataList { get; set; } =
            new List<AbstractBuffActionData>();
    }
}

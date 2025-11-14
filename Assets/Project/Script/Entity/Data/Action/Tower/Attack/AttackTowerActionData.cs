using System.Collections.Generic;

namespace Mgfirefox.CrisisTd
{
    public class AttackTowerActionData : AbstractTowerActionData
    {
        public IList<AbstractAttackActionData> ActionDataList { get; set; } =
            new List<AbstractAttackActionData>();
    }
}

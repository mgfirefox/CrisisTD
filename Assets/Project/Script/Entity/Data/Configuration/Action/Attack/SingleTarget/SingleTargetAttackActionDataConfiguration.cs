using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    [CreateAssetMenu(fileName = "AttackActionDataConfiguration",
        menuName = "DataConfiguration/Action/Attack/SingleTarget")]
    public class SingleTargetAttackActionDataConfiguration : AbstractAttackActionDataConfiguration
    {
        public override AttackActionType Type => AttackActionType.SingleTarget;
    }
}

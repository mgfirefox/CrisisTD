using System.Collections.Generic;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class SingleTargetAttackAction :
        AbstractAttackAction<SingleTargetAttackActionData, ISingleTargetAttackActionView>,
        ISingleTargetAttackAction
    {
        [Inject]
        public SingleTargetAttackAction(ISingleTargetAttackActionView view,
            IEnemyTargetService targetService, ICooldownService cooldownService,
            Scene scene) : base(view, targetService, cooldownService, scene)
        {
        }

        protected override void PerformAttack(IReadOnlyList<IEnemyView> targets)
        {
            IEnemyView target = targets[0];
            target.TakeDamage(Damage);
        }
    }
}

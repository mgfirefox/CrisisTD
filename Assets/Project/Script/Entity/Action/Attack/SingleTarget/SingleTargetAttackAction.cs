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
            IEnemyTargetService targetService, ICooldownService cooldownService, ITowerTransformService transformService, ITowerAnimationService animationService,
            Scene scene) : base(view, targetService, cooldownService, transformService, animationService, scene)
        {
        }

        protected override void PerformAttack(IReadOnlyList<IEnemyView> targets)
        {
            base.PerformAttack(targets);
            
            IEnemyView target = targets[0];
            AnimationService.CreateTracer(target.Position + target.PivotPoint);
            target.TakeDamage(Damage);
        }
    }
}

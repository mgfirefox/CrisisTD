using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class AttackTowerTargetService : TowerTargetService, IAttackTowerTargetService
    {
        [Inject]
        public AttackTowerTargetService(ITowerTransformService transformService,
            ITowerTargetRangeView rangeView, ITowerAllEffectService effectService,
            ITowerView towerView, Scene scene) : base(transformService, effectService, rangeView,
            towerView, scene)
        {
        }

        protected override bool ShouldTargetBeExcluded(ITowerView target)
        {
            if (target.Type == TowerType.Support)
            {
                return true;
            }

            return base.ShouldTargetBeExcluded(target);
        }
    }
}

using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class TowerTargetService : AbstractTargetService<ITowerView>, ITowerTargetService
    {
        private readonly ITowerView towerView;

        [Inject]
        public TowerTargetService(ITowerTransformService transformService,
            ITowerAllEffectService effectService, ITowerTargetRangeView rangeView,
            ITowerView towerView, Scene scene) : base(transformService, effectService, rangeView,
            scene)
        {
        }

        protected override bool ShouldTargetBeExcluded(ITowerView target)
        {
            if (target == towerView)
            {
                return true;
            }

            return base.ShouldTargetBeExcluded(target);
        }
    }
}

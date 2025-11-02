using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class TowerDebuffEffectService : AbstractDebuffEffectService<ITowerView>,
        ITowerDebuffEffectService
    {
        [Inject]
        public TowerDebuffEffectService()
        {
        }

        protected override bool BelongsToSameGroup(ITowerView source, ITowerView groupSource,
            float epsilon)
        {
            if (source.Id != groupSource.Id)
            {
                return false;
            }

            return base.BelongsToSameGroup(source, groupSource, epsilon);
        }
    }
}

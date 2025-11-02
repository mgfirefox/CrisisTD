using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class TowerBuffEffectService : AbstractBuffEffectService<ITowerView>,
        ITowerBuffEffectService
    {
        [Inject]
        public TowerBuffEffectService()
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

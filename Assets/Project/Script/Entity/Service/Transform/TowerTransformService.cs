using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class TowerTransformService : AbstractTransformService<TowerTransformServiceData>,
        ITowerTransformService
    {
        [Inject]
        public TowerTransformService(Scene scene) : base(scene)
        {
        }
    }
}

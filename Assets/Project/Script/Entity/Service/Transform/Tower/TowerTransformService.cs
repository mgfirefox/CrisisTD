using VContainer;

namespace Mgfirefox.CrisisTd
{
    public class TowerTransformService : AbstractTransformService<TowerTransformServiceData>,
        ITowerTransformService
    {
        private readonly IRotationService rotationService;
        
        [Inject]
        public TowerTransformService(IRotationService rotationService, Scene scene) : base(scene)
        {
            this.rotationService = rotationService;
        }

        public void RotateTo(IEnemyView enemy)
        {
            Yaw = rotationService.RotateTo(Position, enemy.Position).eulerAngles.y;
        }
    }
}

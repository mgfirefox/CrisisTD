namespace Mgfirefox.CrisisTd
{
    public interface ITowerTransformService : ITransformService<TowerTransformServiceData>,
        ITowerTransformModel
    {
        void RotateTo(IEnemyView enemy);
    }
}

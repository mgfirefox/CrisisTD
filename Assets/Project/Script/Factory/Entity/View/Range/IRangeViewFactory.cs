namespace Mgfirefox.CrisisTd
{
    public interface IRangeViewFactory : IFactory
    {
        IRangeView Create(TowerType towerType,
            AbstractTowerActionDataConfiguration towerActionDataConfiguration,
            IUnitySceneObject parent);

        bool TryCreate(TowerType towerType,
            AbstractTowerActionDataConfiguration towerActionDataConfiguration,
            IUnitySceneObject parent, out IRangeView view);
    }
}

namespace Mgfirefox.CrisisTd
{
    public interface ITowerPlacementActionFactory : IFactory
    {
        ITowerPlacementAction Create(TowerPlacementActionData data, IUnitySceneObject parent);

        bool TryCreate(TowerPlacementActionData data, IUnitySceneObject parent,
            out ITowerPlacementAction action);
    }
}

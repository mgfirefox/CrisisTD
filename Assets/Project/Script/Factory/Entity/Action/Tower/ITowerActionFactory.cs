namespace Mgfirefox.CrisisTd
{
    public interface ITowerActionFactory : IFactory
    {
        ITowerAction Create(TowerType type, AbstractTowerActionData data, IUnitySceneObject parent);

        bool TryCreate(TowerType type, AbstractTowerActionData data, IUnitySceneObject parent,
            out ITowerAction action);
    }
}

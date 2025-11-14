namespace Mgfirefox.CrisisTd
{
    public interface ITowerInteractionActionFactory : IFactory
    {
        ITowerInteractionAction Create(TowerInteractionActionData data, IUnitySceneObject parent);

        bool TryCreate(TowerInteractionActionData data, IUnitySceneObject parent,
            out ITowerInteractionAction action);
    }
}

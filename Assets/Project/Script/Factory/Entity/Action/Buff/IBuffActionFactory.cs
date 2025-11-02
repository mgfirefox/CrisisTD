namespace Mgfirefox.CrisisTd
{
    public interface IBuffActionFactory : IFactory
    {
        IBuffAction Create(AbstractBuffActionData data, IUnitySceneObject parent);

        bool TryCreate(AbstractBuffActionData data, IUnitySceneObject parent,
            out IBuffAction action);
    }
}

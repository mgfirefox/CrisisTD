namespace Mgfirefox.CrisisTd
{
    public interface IAttackActionFactory : IFactory
    {
        IAttackAction Create(AbstractAttackActionData data, IUnitySceneObject parent);

        bool TryCreate(AbstractAttackActionData data, IUnitySceneObject parent,
            out IAttackAction action);
    }
}

namespace Mgfirefox.CrisisTd
{
    public interface IAttackAction : IAction
    {
    }

    public interface IAttackAction<in TData> : IAction<TData>, IAttackAction
        where TData : AbstractAttackActionData
    {
    }
}

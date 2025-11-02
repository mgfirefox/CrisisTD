namespace Mgfirefox.CrisisTd
{
    public interface IBuffAction : IAction
    {
    }

    public interface IBuffAction<in TData> : IAction<TData>, IBuffAction
        where TData : AbstractBuffActionData
    {
    }
}

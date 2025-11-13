namespace Mgfirefox.CrisisTd
{
    public interface IUiAction : IAction
    {
    }

    public interface IUiAction<in TData> : IUiAction, IAction<TData>
        where TData : AbstractActionData
    {
    }
}

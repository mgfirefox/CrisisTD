namespace Mgfirefox.CrisisTd
{
    public interface IAction : IPresenter
    {
        void Perform();
    }

    public interface IAction<in TData> : IPresenter<TData>, IAction
        where TData : AbstractActionData
    {
    }
}

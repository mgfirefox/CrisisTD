namespace Mgfirefox.CrisisTd
{
    public interface IUiPresenter : IPresenter
    {
    }

    public interface IUiPresenter<in TData> : IUiPresenter, IPresenter<TData>
        where TData : AbstractData
    {
    }
}

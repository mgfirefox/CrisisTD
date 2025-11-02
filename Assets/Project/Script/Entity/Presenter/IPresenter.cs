namespace Mgfirefox.CrisisTd
{
    public interface IPresenter : ISceneObject
    {
    }

    public interface IPresenter<in TData> : ISceneObject<TData>, IPresenter
        where TData : AbstractData
    {
    }
}

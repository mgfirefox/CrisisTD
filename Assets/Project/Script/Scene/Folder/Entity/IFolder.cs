namespace Mgfirefox.CrisisTd
{
    public interface IFolder : ISceneFolder
    {
    }

    public interface IFolder<out TIItem> : ISceneFolder<TIItem>, IFolder
        where TIItem : class, IView
    {
    }
}

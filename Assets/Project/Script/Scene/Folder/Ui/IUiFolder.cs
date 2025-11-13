namespace Mgfirefox.CrisisTd
{
    public interface IUiFolder : ISceneFolder
    {
    }

    public interface IUiFolder<out TIItem> : ISceneFolder<TIItem>, IUiFolder
        where TIItem : class, IUi
    {
    }
}

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractFolder : AbstractSceneFolder, IFolder
    {
    }

    public abstract class AbstractFolder<TIItem> : AbstractSceneFolder<TIItem>, IFolder<TIItem>
        where TIItem : class, IView
    {
    }
}

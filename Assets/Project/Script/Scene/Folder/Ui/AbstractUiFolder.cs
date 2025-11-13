namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractUiFolder : AbstractSceneFolder, IUiFolder
    {
    }

    public abstract class AbstractUiFolder<TIItem> : AbstractSceneFolder<TIItem>, IUiFolder<TIItem>
        where TIItem : class, IUi
    {
    }
}

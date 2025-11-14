namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractActionUiFolder : AbstractUiFolder, IActionUiFolder
    {
    }

    public abstract class AbstractActionUiFolder<TIItem> : AbstractUiFolder<TIItem>,
        IActionUiFolder<TIItem>
        where TIItem : class, IActionUi
    {
    }
}

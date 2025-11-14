namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractActionFolder : AbstractFolder, IActionFolder
    {
    }

    public abstract class AbstractActionFolder<TIItem> : AbstractFolder<TIItem>,
        IActionFolder<TIItem>
        where TIItem : class, IActionView
    {
    }
}

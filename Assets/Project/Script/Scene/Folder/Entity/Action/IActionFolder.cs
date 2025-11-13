namespace Mgfirefox.CrisisTd
{
    public interface IActionFolder : IFolder
    {
    }

    public interface IActionFolder<out TIItem> : IFolder<TIItem>, IActionFolder
        where TIItem : class, IActionView
    {
    }
}

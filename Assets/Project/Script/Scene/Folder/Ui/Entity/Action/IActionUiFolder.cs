namespace Mgfirefox.CrisisTd
{
    public interface IActionUiFolder : IUiFolder
    {
    }

    public interface IActionUiFolder<out TIItem> : IUiFolder<TIItem>, IActionUiFolder
        where TIItem : class, IActionUi
    {
    }
}

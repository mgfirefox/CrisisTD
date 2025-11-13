namespace Mgfirefox.CrisisTd
{
    public interface ITowerActionUi : IActionUi
    {
    }

    public interface ITowerActionUi<in TIView> : ITowerActionUi
        where TIView : class, ITowerActionView
    {
        TIView View { set; }
    }
}

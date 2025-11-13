namespace Mgfirefox.CrisisTd
{
    public interface IBuffActionUi : IActionUi
    {
    }

    public interface IBuffActionUi<in TIView> : IBuffActionUi
        where TIView : class, IBuffActionView
    {
        TIView View { set; }
    }
}

namespace Mgfirefox.CrisisTd
{
    public interface IAttackActionUi : IActionUi
    {
    }

    public interface IAttackActionUi<in TIView> : IAttackActionUi
        where TIView : class, IAttackActionView
    {
        TIView View { set; }
    }
}

namespace Mgfirefox.CrisisTd
{
    public interface IAttackTowerActionView : ITowerActionView, IAttackTowerActionModel
    {
        IAttackActionFolder ActionFolder { get; }
    }
}

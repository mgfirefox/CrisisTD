namespace Mgfirefox.CrisisTd
{
    public interface IAttackTowerActionView : ITowerActionView, IAttackTowerActionModel
    {
        IAttackActionFolderView ActionViewFolder { get; }
    }
}

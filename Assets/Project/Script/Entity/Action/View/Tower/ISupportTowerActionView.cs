namespace Mgfirefox.CrisisTd
{
    public interface ISupportTowerActionView : ITowerActionView, ISupportTowerActionModel
    {
        IBuffActionFolderView ActionViewFolder { get; }
    }
}

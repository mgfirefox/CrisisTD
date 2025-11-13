namespace Mgfirefox.CrisisTd
{
    public interface ISupportTowerActionView : ITowerActionView, ISupportTowerActionModel
    {
        IBuffActionFolder ActionFolder { get; }
    }
}

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractTowerActionData : AbstractActionData
    {
        public TargetServiceData TargetServiceData { get; set; } = new();
    }
}

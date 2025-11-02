namespace Mgfirefox.CrisisTd
{
    public class TargetServiceData : AbstractServiceData, ITargetModel
    {
        public float RangeRadius { get; set; }

        public TargetPriority TargetPriority { get; set; } = TargetPriority.Undefined;
    }
}

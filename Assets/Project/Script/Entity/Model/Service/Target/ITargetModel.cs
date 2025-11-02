namespace Mgfirefox.CrisisTd
{
    public interface ITargetModel : IModel
    {
        public float RangeRadius { get; }

        public TargetPriority TargetPriority { get; }
    }
}

namespace Mgfirefox.CrisisTd
{
    public interface IAreaAttackActionModel : IAttackActionModel
    {
        public float InnerRadius { get; }
        public float OuterRadius { get; }

        public int MaxHitCount { get; }
    }
}

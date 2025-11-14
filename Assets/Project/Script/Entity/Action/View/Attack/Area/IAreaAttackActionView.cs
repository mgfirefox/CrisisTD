namespace Mgfirefox.CrisisTd
{
    public interface IAreaAttackActionView : IAttackActionView, IAreaAttackActionModel
    {
        new float InnerRadius { get; set; }
        new float OuterRadius { get; set; }

        new int MaxHitCount { get; set; }
    }
}

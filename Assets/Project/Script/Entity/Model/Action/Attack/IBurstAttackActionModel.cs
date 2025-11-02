namespace Mgfirefox.CrisisTd
{
    public interface IBurstAttackActionModel : IAttackActionModel, IBurstCooldownModel
    {
        int MaxBurstShotCount { get; }
        int BurstShotCount { get; }
    }
}

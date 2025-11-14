namespace Mgfirefox.CrisisTd
{
    public interface IBurstAttackActionView : IAttackActionView, IBurstAttackActionModel
    {
        new int MaxBurstShotCount { get; set; }
        new int BurstShotCount { get; set; }

        new float BurstMaxCooldown { get; set; }
        new float BurstCooldown { get; set; }
    }
}

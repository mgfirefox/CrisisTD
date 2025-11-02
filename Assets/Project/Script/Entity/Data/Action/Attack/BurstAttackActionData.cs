namespace Mgfirefox.CrisisTd
{
    public class BurstAttackActionData : AbstractAttackActionData
    {
        public override AttackActionType Type => AttackActionType.Burst;

        public int MaxBurstShotCount { get; set; }
        public int BurstShotCount { get; set; }

        public CooldownServiceData BurstCooldownServiceData { get; set; } = new();
    }
}

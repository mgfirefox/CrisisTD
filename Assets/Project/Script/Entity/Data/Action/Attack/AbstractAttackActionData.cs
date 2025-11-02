namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractAttackActionData : AbstractActionData
    {
        public abstract AttackActionType Type { get; }

        public float Damage { get; set; }

        public CooldownServiceData CooldownServiceData { get; set; } = new();
    }
}

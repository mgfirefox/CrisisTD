namespace Mgfirefox.CrisisTd
{
    public class CooldownServiceData : AbstractServiceData, ICooldownModel
    {
        public float MaxCooldown { get; set; }
        public float Cooldown { get; set; }
    }
}

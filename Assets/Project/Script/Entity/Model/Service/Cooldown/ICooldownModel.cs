namespace Mgfirefox.CrisisTd
{
    public interface ICooldownModel : IModel
    {
        public float MaxCooldown { get; }
        public float Cooldown { get; }
    }
}

namespace Mgfirefox.CrisisTd
{
    public interface IBurstCooldownModel : IModel
    {
        public float BurstMaxCooldown { get; }
        public float BurstCooldown { get; }
    }
}

namespace Mgfirefox.CrisisTd
{
    public interface IHealthModel : IModel
    {
        public float MaxHealth { get; }
        public float Health { get; }

        public bool IsDied { get; }
    }
}

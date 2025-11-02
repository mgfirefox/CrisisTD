namespace Mgfirefox.CrisisTd
{
    public interface IAttackActionModel : IModel, ICooldownModel
    {
        public float Damage { get; }
    }
}

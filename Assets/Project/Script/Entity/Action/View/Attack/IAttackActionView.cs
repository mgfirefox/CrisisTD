namespace Mgfirefox.CrisisTd
{
    public interface IAttackActionView : IActionView, IAttackActionModel
    {
        new float Damage { get; set; }

        new float MaxCooldown { get; set; }
        new float Cooldown { get; set; }
    }
}

namespace Mgfirefox.CrisisTd
{
    public interface IBuffActionView : IActionView, IBuffActionModel
    {
        new EffectType Type { get; set; }
        new float MaxValue { get; set; }
        new float Value { get; set; }
    }
}

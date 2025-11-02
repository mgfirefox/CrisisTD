namespace Mgfirefox.CrisisTd
{
    public interface IBuffActionModel : IModel
    {
        EffectType Type { get; }
        float MaxValue { get; }
        float Value { get; }
    }
}

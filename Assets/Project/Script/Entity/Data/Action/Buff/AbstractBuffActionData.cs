namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractBuffActionData : AbstractActionData
    {
        public abstract BuffActionType Type { get; }

        public EffectType BuffType { get; set; }
        public float MaxValue { get; set; }
        public float Value { get; set; }
    }
}

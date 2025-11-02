namespace Mgfirefox.CrisisTd
{
    /*
     * Value [0.0f, inf) represents Buff effect
     */
    public class BuffEffect : Effect
    {
        public BuffEffect() : base(EffectKind.Buff)
        {
            Value = 0.0f;
        }

        public override object Clone()
        {
            var effect = new BuffEffect
            {
                Type = Type,
                Value = Value,
            };

            return effect;
        }
    }
}

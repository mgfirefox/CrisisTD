namespace Mgfirefox.CrisisTd
{
    /*
     * Value [0.0f, 1.0f] represents Debuff effect
     */
    public class DebuffEffect : Effect
    {
        public DebuffEffect() : base(EffectKind.Debuff)
        {
            Value = 0.0f;
        }

        public override object Clone()
        {
            var effect = new DebuffEffect
            {
                Type = Type,
                Value = Value,
            };

            return effect;
        }
    }
}

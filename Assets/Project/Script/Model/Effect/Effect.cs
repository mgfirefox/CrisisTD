using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    /*
     * Value [0.0f, 1.0f) represents Debuff effect
     * Value 1.0f represents no effect
     * Value (1.0f, inf) represents Buff effect
     */
    [Serializable]
    public class Effect : ICloneable
    {
        [SerializeField]
        private EffectType type;
        [SerializeField]
        private EffectKind kind;
        [SerializeField]
        private float value;

        public EffectType Type { get => type; set => type = value; }
        public EffectKind Kind => kind;
        public float Value { get => value; set => this.value = value; }

        public Effect()
        {
            kind = EffectKind.Effect;
            Value = 1.0f;
        }

        public Effect(EffectKind kind)
        {
            this.kind = kind;
        }

        public virtual object Clone()
        {
            var effect = new Effect(kind)
            {
                type = type,
                value = value,
            };

            return effect;
        }
    }
}

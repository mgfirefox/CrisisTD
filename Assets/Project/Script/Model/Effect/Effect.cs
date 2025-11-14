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

        protected bool Equals(Effect other)
        {
            return type == other.type && kind == other.kind && value.Equals(other.value);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((Effect)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int)type, (int)kind, value);
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

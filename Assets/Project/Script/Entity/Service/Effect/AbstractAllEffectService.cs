using System;
using System.Collections.Generic;

namespace Mgfirefox.CrisisTd
{
    public abstract class
        AbstractAllEffectService<TData, TISourceView> : AbstractDataService<TData>,
        IAllEffectService<TData, TISourceView>
        where TData : AbstractAllEffectServiceData
        where TISourceView : class, IView
    {
        private Effect rangeEffect = new()
        {
            Type = EffectType.Range,
            Value = 1.0f,
        };

        public Effect RangeEffect
        {
            get => rangeEffect.Clone() as Effect;
            private set => rangeEffect = value.Clone() as Effect;
        }

        public event Action<Effect> Changed;

        public event Action<Effect> RangeChanged;

        protected AbstractAllEffectService(Scene scene) : base(scene)
        {
        }

        public void Apply(Effect effect, TISourceView source)
        {
            float epsilon = Scene.Settings.MathSettings.Epsilon;

            if (!EffectValidator.TryValidate(effect, epsilon))
            {
                // TODO: Change Exception
                throw new InvalidArgumentException(nameof(effect), effect.ToString());
            }

            if (source == null)
            {
                throw new InvalidArgumentException(nameof(source), null);
            }

            switch (effect.Kind)
            {
                case EffectKind.Buff:
                    ApplyBuff(effect.Clone() as BuffEffect, source);
                break;
                case EffectKind.Debuff:
                    ApplyDebuff(effect.Clone() as DebuffEffect, source);
                break;
                case EffectKind.Effect:
                    ApplyEffect(effect.Clone() as Effect, source);
                break;
                case EffectKind.Undefined:
                default:
                    // TODO: Change Exception
                    throw new InvalidArgumentException(nameof(effect), effect.ToString());
            }
        }

        public void Reapply()
        {
            RangeChanged?.Invoke(RangeEffect);
        }

        protected virtual void ApplyBuff(BuffEffect buffEffect, TISourceView source)
        {
        }

        protected virtual void ApplyDebuff(DebuffEffect debuffEffect, TISourceView source)
        {
        }

        protected virtual void ApplyEffect(Effect effect, TISourceView source)
        {
        }

        protected Effect CalculateNewEffect(IList<Effect> effects)
        {
            float epsilon = Scene.Settings.MathSettings.Epsilon;

            if (effects == null)
            {
                throw new InvalidArgumentException(nameof(effects), null);
            }
            if (effects.Count == 0)
            {
                return new Effect();
            }

            var newEffect = new Effect
            {
                Type = effects[0].Type,
                Value = 1.0f,
            };

            foreach (Effect effect in effects)
            {
                if (effect.Type != newEffect.Type)
                {
                    // TODO: Change Exception
                    throw new InvalidArgumentException(nameof(effects), effects.ToString());
                }
                if (!EffectValidator.TryValidate(effect, epsilon))
                {
                    // TODO: Change Exception
                    throw new InvalidArgumentException(nameof(effects), effects.ToString());
                }

                switch (effect.Kind)
                {
                    case EffectKind.Effect:
                        newEffect.Value += effect.Value - 1.0f;
                    break;
                    case EffectKind.Buff:
                        newEffect.Value += effect.Value;
                    break;
                    case EffectKind.Debuff:
                        newEffect.Value -= effect.Value;
                    break;
                    case EffectKind.Undefined:
                    default:
                        // TODO: Change Exception
                        throw new InvalidArgumentException(nameof(effects), effects.ToString());
                }
            }

            if (newEffect.Value.IsLessThanApproximately(Constant.minEffectValue, epsilon))
            {
                newEffect.Value = Constant.minEffectValue;
            }

            return newEffect;
        }

        protected virtual void OnEffectChanged<TEffect>(TEffect effect)
            where TEffect : Effect
        {
            float epsilon = Scene.Settings.MathSettings.Epsilon;

            if (!EffectValidator.TryValidate(effect, epsilon))
            {
                // TODO: Change Exception
                throw new InvalidArgumentException(nameof(effect), effect.ToString());
            }
        }

        protected void InvokeEffectChanged(Effect effect)
        {
            float epsilon = Scene.Settings.MathSettings.Epsilon;

            if (!EffectValidator.TryValidate(effect, epsilon))
            {
                // TODO: Change Exception
                throw new InvalidArgumentException(nameof(effect), effect.ToString());
            }

            switch (effect.Type)
            {
                case EffectType.Range:
                    RangeEffect = effect.Clone() as Effect;

                    RangeChanged?.Invoke(RangeEffect);
                break;
                case EffectType.Undefined:
                default:
                    throw new InvalidArgumentException(nameof(effect), effect.ToString());
            }

            Changed?.Invoke(effect.Clone() as Effect);
        }
    }
}

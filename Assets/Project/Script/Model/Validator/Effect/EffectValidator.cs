using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public static class EffectValidator
    {
        public static void Validate(Effect effect, float epsilon)
        {
            if (effect == null)
            {
                throw new InvalidArgumentException(nameof(effect), null);
            }

            if (!EffectTypeValidator.TryValidate(effect.Type))
            {
                // TODO: Change Exception
                throw new InvalidArgumentException(nameof(effect), effect.ToString());
            }

            if (!EffectKindValidator.TryValidate(effect.Kind))
            {
                // TODO: Change Exception
                throw new InvalidArgumentException(nameof(effect), effect.ToString());
            }

            switch (effect.Kind)
            {
                case EffectKind.Effect:
                    if (effect.Value.IsLessThanApproximately(Constant.minEffectValue, epsilon) ||
                        effect.Value.IsGreaterThanApproximately(Constant.maxEffectValue, epsilon))
                    {
                        // TODO: Change Exception
                        throw new InvalidArgumentException(nameof(effect), effect.ToString());
                    }
                break;
                case EffectKind.Buff:
                    if (effect.Value.IsLessThanApproximately(Constant.minBuffEffectValue,
                            epsilon) ||
                        effect.Value.IsGreaterThanApproximately(Constant.maxBuffEffectValue,
                            epsilon))
                    {
                        // TODO: Change Exception
                        throw new InvalidArgumentException(nameof(effect), effect.ToString());
                    }
                break;
                case EffectKind.Debuff:
                    if (effect.Value.IsLessThanApproximately(Constant.minDebuffEffectValue,
                            epsilon) ||
                        effect.Value.IsGreaterThanApproximately(Constant.maxDebuffEffectValue,
                            epsilon))
                    {
                        // TODO: Change Exception
                        throw new InvalidArgumentException(nameof(effect), effect.ToString());
                    }
                break;
                case EffectKind.Undefined:
                default:
                    // TODO: Change Exception
                    throw new InvalidArgumentException(nameof(effect), effect.ToString());
            }
        }

        public static bool TryValidate(Effect effect, float epsilon)
        {
            try
            {
                Validate(effect, epsilon);

                return true;
            }
            catch (Exception e)
            {
                // TODO: Change Exception
                if (e is not Exception)
                {
                    Debug.LogException(new CaughtUnexpectedException(e));
                }
            }

            return false;
        }
    }
}

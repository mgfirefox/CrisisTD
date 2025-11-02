using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public static class EffectTypeValidator
    {
        public static IReadOnlySet<EffectType> ValidTypes { get; } = new HashSet<EffectType>
        {
            EffectType.Range,
        };

        public static void Validate(EffectType type)
        {
            if (ValidTypes.Contains(type))
            {
                return;
            }

            throw new ValidationException(type.ToString(), "Effect");
        }

        public static bool TryValidate(EffectType type)
        {
            try
            {
                Validate(type);

                return true;
            }
            catch (Exception e)
            {
                if (e is not ValidationException)
                {
                    Debug.LogException(new CaughtUnexpectedException(e));
                }
            }

            return false;
        }
    }
}

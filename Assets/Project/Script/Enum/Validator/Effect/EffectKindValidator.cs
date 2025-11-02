using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public static class EffectKindValidator
    {
        public static IReadOnlySet<EffectKind> ValidKinds { get; } = new HashSet<EffectKind>
        {
            EffectKind.Effect,
            EffectKind.Buff,
            EffectKind.Debuff,
        };

        public static void Validate(EffectKind kind)
        {
            if (ValidKinds.Contains(kind))
            {
                return;
            }

            throw new ValidationException(kind.ToString(), "Effect");
        }

        public static bool TryValidate(EffectKind type)
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
